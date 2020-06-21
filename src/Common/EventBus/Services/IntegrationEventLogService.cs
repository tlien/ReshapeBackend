using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using Reshape.Common.EventBus.Events;

namespace Reshape.Common.EventBus.Services
{
    /// <summary>
    /// Maintains a log of integration events going out of the event bus publisher (assembly) it is registered in.
    /// </summary>
    public class IntegrationEventLogService : IIntegrationEventLogService
    {
        private readonly IntegrationEventLogContext _integrationEventLogContext;
        private readonly DbConnection _dbConnection;
        private static readonly IReadOnlyCollection<Type> _eventTypes;

        // Static constructor used to initialize eventTypes collection.
        // Since the use case of this class require a transient scope
        // it seems wise to not recreate this collection as reflection is expensive.
        static IntegrationEventLogService()
        {
            // Use reflection to find all implementations of the IIntegrationEvent interface.
            // This is used to deserialize a json encoded representation of an integration event back into its original class.
            _eventTypes = Assembly.Load(Assembly.GetEntryAssembly().FullName)
                .GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(IIntegrationEvent)))
                .ToList()
                .AsReadOnly();
        }

        public IntegrationEventLogService(DbConnection dbConnection)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
            _integrationEventLogContext = new IntegrationEventLogContext(
                new DbContextOptionsBuilder<IntegrationEventLogContext>()
                    .UseNpgsql(_dbConnection).UseSnakeCaseNamingConvention()
                    .Options);
        }

        /// <summary>
        /// Get integration events marked as 'NotPublished(0)' from the log database.
        /// </summary>
        /// <param name="transactionId">The desired transaction id of the integration events to retrieve.</param>
        /// <returns>An <c>IEnumerable containing the events logged with a given transaction id</c></returns>
        public async Task<IEnumerable<IntegrationEventLogEntry>> RetrieveEventLogsPendingToPublishAsync(Guid transactionId)
        {
            var tid = transactionId.ToString();

            var result = await _integrationEventLogContext.IntegrationEventLogs
                .Where(e => e.TransactionId == tid && e.State == EventStateEnum.NotPublished).ToListAsync();

            if (result != null && result.Any())
            {
                // Deserialize all found events and save the type along with the deserialized class in the IntegrationEventLogEntry.
                return result.OrderBy(o => o.TimeStamp)
                    .Select(e => e.DeserializeJsonContent(_eventTypes.FirstOrDefault(t => t.Name == e.EventTypeShortName)));
            }

            return new List<IntegrationEventLogEntry>();
        }

        /// <summary>
        /// Save event to log along with the transaction id of the transaction in which the event was created.
        /// </summary>
        public Task SaveEventAsync(IIntegrationEvent @event, IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));

            var eventLogEntry = new IntegrationEventLogEntry(@event, transaction.TransactionId);

            // Set the datebase transaction to the provided instance, this ensure integration events
            // are logged in the same transaction as the domain data change that spawned the event.
            _integrationEventLogContext.Database.UseTransaction(transaction.GetDbTransaction());
            _integrationEventLogContext.IntegrationEventLogs.Add(eventLogEntry);

            return _integrationEventLogContext.SaveChangesAsync();
        }

        /// <summary>
        /// Marks an event as in progress, meaning it has been retrieved from the log and is awaiting publishing.!--
        /// This also increments the <c>TimesSent</c> property of the event in order to track repeated attempts at publishing.
        /// </summary>
        /// <param name="eventId">The id of the event to mark as in progress</param>
        public Task MarkEventAsInProgressAsync(Guid eventId)
        {
            return UpdateEventStatus(eventId, EventStateEnum.InProgress);
        }

        /// <summary>
        /// Marks an event as published, meaning it has been successfully sent by the eventbus.
        /// </summary>
        /// <param name="eventId">The id of the event to mark as published</param>
        public Task MarkEventAsPublishedAsync(Guid eventId)
        {
            return UpdateEventStatus(eventId, EventStateEnum.Published);
        }

        /// <summary>
        /// Marks an event as failed, meaning the event bus could not publish the message.
        /// </summary>
        /// <param name="eventId">The id of the event to mark as failed</param>
        public Task MarkEventAsFailedAsync(Guid eventId)
        {
            return UpdateEventStatus(eventId, EventStateEnum.PublishedFailed);
        }

        private Task UpdateEventStatus(Guid eventId, EventStateEnum status)
        {
            var eventLogEntry = _integrationEventLogContext.IntegrationEventLogs.Single(ie => ie.EventId == eventId);
            eventLogEntry.State = status;

            // If the event fails to send due to a crash or disconnect or similar, it should be retried.
            // This keeps track of the number of tries.
            if (status == EventStateEnum.InProgress)
                eventLogEntry.TimesSent++;

            _integrationEventLogContext.IntegrationEventLogs.Update(eventLogEntry);

            return _integrationEventLogContext.SaveChangesAsync();
        }
    }
}
