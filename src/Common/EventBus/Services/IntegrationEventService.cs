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

        public async Task AddAndSaveEventAsync<T>(T evt) where T : IIntegrationEvent
        {
            _logger.LogDebug("Storing event for processing. Details: {0}", evt);
            await _integrationEventLogService.SaveEventAsync(evt, _dbContext.GetCurrentTransaction());
        }

        public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
        {
            var pendingEvents = await _integrationEventLogService.RetrieveEventLogsPendingToPublishAsync(transactionId);

            foreach (var logEvent in pendingEvents)
            {
                try
                {
                    await _integrationEventLogService.MarkEventAsInProgressAsync(logEvent.EventId);
                    _logger.LogDebug("Event {0} status marked as in progress.", logEvent.EventId);

                    await _eventBus.PublishIntegrationEvent(logEvent.IntegrationEvent, logEvent.EventType);
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