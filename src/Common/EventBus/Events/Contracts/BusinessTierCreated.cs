using System;

namespace Reshape.Common.EventBus.Events.Contracts
{
    public interface BusinessTierCreated : IIntegrationEvent
    {
        Guid Id { get; }
        string Name { get; }
        string Description { get; }
        decimal Price { get; }
    }
}