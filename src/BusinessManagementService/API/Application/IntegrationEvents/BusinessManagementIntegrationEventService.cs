using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using Reshape.BusinessManagementService.Infrastructure;
using Reshape.Common.EventBus;
using Reshape.Common.EventBus.Events;
using Reshape.Common.EventBus.Services;

namespace Reshape.BusinessManagementService.API.Application.IntegrationEvents
{
    public class BusinessManagementIntegrationEventService : IBusinessManagementIntegrationEventService
    {
        private readonly IBusControl _eventBus;
        private readonly BusinessManagementContext _bmContext;
        private readonly IIntegrationEventLogService _integrationEventLogService;
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventTracker _eventTracker;
        public BusinessManagementIntegrationEventService(
            IBusControl eventBus,
            BusinessManagementContext bmContext, 
            IntegrationEventLogContext integrationEventLogContext,
            Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory,
            IEventTracker eventTracker)
        {
            _bmContext = bmContext ?? throw new ArgumentNullException(nameof(bmContext));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _integrationEventLogService = _integrationEventLogServiceFactory(_bmContext.Database.GetDbConnection());
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventTracker = eventTracker;
        }
        
        public async Task AddAndSaveEventAsync(IntegrationEvent evt)
        {
            Console.WriteLine("Storing event for processing. Details: {0}", evt);
            await _integrationEventLogService.SaveEventAsync(evt, _bmContext.GetCurrentTransaction());
        }

        public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
        {
            var pendingEvents = await _integrationEventLogService.RetrieveEventLogsPendingToPublishAsync(transactionId);
            
            foreach(var logEvent in pendingEvents)
            {
                try 
                {
                    await _integrationEventLogService.MarkEventAsInProgressAsync(logEvent.EventId);
                    Console.WriteLine("Event {0} status marked as in progress.", logEvent.EventId);

                    Type eventType = _eventTracker.GetEventTypeByName(logEvent.EventTypeShortName);
                    await _eventBus.PublishIntegrationEvent(logEvent.Content, eventType);
                    Console.WriteLine("Published event {0} with content: {1}", logEvent.EventId, logEvent.Content);

                    await _integrationEventLogService.MarkEventAsPublishedAsync(logEvent.EventId);
                    Console.WriteLine("Event {0} status marked as published.", logEvent.EventId);
                } 
                catch(Exception ex)
                {
                    await _integrationEventLogService.MarkEventAsFailedAsync(logEvent.EventId);
                    Console.WriteLine("Event {0} status marked as failed. Exception: {1}", logEvent.EventId, ex);
                }
            }
        }
    }
}