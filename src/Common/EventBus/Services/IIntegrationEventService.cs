using System;
using System.Threading.Tasks;

using Reshape.Common.EventBus.Events;

namespace Reshape.Common.EventBus.Services
{
    public interface IIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(IntegrationEvent evt);
    }
}