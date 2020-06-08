using System;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MassTransit;

using Reshape.Common.EventBus;
using Reshape.Common.EventBus.Events;
using Reshape.Common.EventBus.Services;
using Reshape.BusinessManagementService.Infrastructure;

namespace Reshape.BusinessManagementService.API.Application.IntegrationEvents
{
    public class BusinessManagementIntegrationEventService : IBusinessManagementIntegrationEventService
    {
        private readonly ILogger _logger;
        private readonly IBusControl _eventBus;
        private readonly BusinessManagementContext _bmContext;
        private readonly IIntegrationEventLogService _integrationEventLogService;
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventTracker _eventTracker;
        public BusinessManagementIntegrationEventService(
            IBusControl eventBus,
            BusinessManagementContext bmContext,
            Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory,
            IEventTracker eventTracker,
            ILogger<BusinessManagementIntegrationEventService> logger)
        {
            _logger = logger;
            _bmContext = bmContext ?? throw new ArgumentNullException(nameof(bmContext));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _integrationEventLogService = _integrationEventLogServiceFactory(_bmContext.Database.GetDbConnection());
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventTracker = eventTracker;
        }

        public async Task AddAndSaveEventAsync(IntegrationEvent evt)
        {
            _logger.LogDebug("Storing event for processing. Details: {0}", evt);
            await _integrationEventLogService.SaveEventAsync(evt, _bmContext.GetCurrentTransaction());
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

                    Type eventType = _eventTracker.GetEventTypeByName(logEvent.EventTypeShortName);
                    await _eventBus.PublishIntegrationEvent(logEvent.Content, eventType);
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