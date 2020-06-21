using System;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MassTransit;

using Reshape.Common.SeedWork;
using Reshape.Common.EventBus.Events;

namespace Reshape.Common.EventBus.Services
{
    /// <summary>
    /// Facilitates publishing events to the event bus along with logging outgoing messages in a DDD compliant manner.
    /// Events published through this service will be easily correlated to the transaction during which they were created.
    /// </summary>
    /// <typeparam name="TDbContext">
    /// The DbContext used for logging events.
    ///
    /// **Must implement the <c>DbContext</c> and <c>IUnitOfWork</c> interfaces.**
    /// </typeparam>
    public class IntegrationEventService<TDbContext> : IIntegrationEventService where TDbContext : DbContext, IUnitOfWork
    {
        private readonly ILogger _logger;
        private readonly IBusControl _eventBus;
        private readonly TDbContext _dbContext;
        private readonly IIntegrationEventLogService _integrationEventLogService;
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;

        public IntegrationEventService(
            IBusControl eventBus,
            TDbContext dbContext,
            Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory,
            ILogger<IntegrationEventService<TDbContext>> logger)
        {
            _logger = logger;
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _integrationEventLogService = _integrationEventLogServiceFactory(_dbContext.Database.GetDbConnection());
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        /// <summary>
        /// Persist given event to integration event log database.
        /// For details on how the persisting works and why it might fail, refer to the class summary.
        /// </summary>
        /// <param name="@event">The integration event to persist.</param>
        /// <typeparam name="TEvent">The type of the integration event.</typeparam>
        public async Task AddAndSaveEventAsync<TEvent>(TEvent @event) where TEvent : IIntegrationEvent
        {
            _logger.LogDebug("Storing event for processing. Details: {0}", @event);
            await _integrationEventLogService.SaveEventAsync(@event, _dbContext.GetCurrentTransaction());
        }

        /// <summary>
        /// Fetches any pending events from the current transaction and publishes these through the event bus one by one.
        /// Each event passes through several states, making it easier to track when and if an event fails to be published.
        /// If any failures are detected, publishing that event will NOT be retried (no logic for handling this exists at this point).
        /// </summary>
        /// <param name="transactionId">The transaction id of the DbContextTransaction the given events where created during.</param>
        public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
        {
            var pendingEvents = await _integrationEventLogService.RetrieveEventLogsPendingToPublishAsync(transactionId);

            foreach (var logEvent in pendingEvents)
            {
                try
                {
                    await _integrationEventLogService.MarkEventAsInProgressAsync(logEvent.EventId);
                    _logger.LogDebug("Event {0} status marked as in progress.", logEvent.EventId);

                    await _eventBus.Publish(logEvent.IntegrationEvent, logEvent.EventType);
                    _logger.LogDebug("Published event {0} with content: {1}", logEvent.EventId, logEvent.Content);

                    await _integrationEventLogService.MarkEventAsPublishedAsync(logEvent.EventId);
                    _logger.LogDebug("Event {0} status marked as published.", logEvent.EventId);
                }
                catch (Exception ex)
                {
                    await _integrationEventLogService.MarkEventAsFailedAsync(logEvent.EventId);
                    _logger.LogError(ex, "Event {0} status marked as failed", logEvent.EventId);
                }
            }
        }
    }
}