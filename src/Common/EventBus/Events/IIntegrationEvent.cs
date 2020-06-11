using System;

namespace Reshape.Common.EventBus.Events
{
    public interface IIntegrationEvent
    {
        Guid EventId { get; }
        DateTime TimeStamp { get; }
    }
}
