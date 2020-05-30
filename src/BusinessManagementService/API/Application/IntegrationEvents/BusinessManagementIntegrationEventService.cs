using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reshape.BusinessManagementService.Infrastructure;
using Reshape.Common.EventBus;
using Reshape.Common.EventBus.Events;
using Reshape.Common.EventBus.Services;

namespace Reshape.BusinessManagementService.API.Application.IntegrationEvents
{
    public class BusinessManagementIntegrationEventService : IBusinessManagementIntegrationEventService
    {
        // private readonly IEventBus _eventBus;
        private readonly BusinessManagementContext _bmContext;
        private readonly IIntegrationEventLogService _integrationEventLogService;
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        public BusinessManagementIntegrationEventService(
            /* IEventBus eventBus, */
            BusinessManagementContext bmContext, 
            IntegrationEventLogContext integrationEventLogContext,
            Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory)
        {
            _bmContext = bmContext ?? throw new ArgumentNullException(nameof(bmContext));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _integrationEventLogService = _integrationEventLogServiceFactory(_bmContext.Database.GetDbConnection());
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
                    // TODO: Publish through MassTransit eventbus to RabbitMQ
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