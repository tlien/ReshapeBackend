using System;
using System.Threading.Tasks;
using Reshape.Common.EventBus.Events;

namespace Reshape.BusinessManagementService.API.Application.IntegrationEvents
{
    public interface IBusinessManagementIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(IntegrationEvent evt);
    }
}