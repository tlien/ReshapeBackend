using System;

namespace Reshape.Common.EventBus.Events
{
    /// <summary>
    /// Base integration event contract.
    /// Holds information used by the IntegrationEventLogService.
    ///
    /// **Not meant to be consumed!**
    /// </summary>
    public interface IIntegrationEvent
    {
        Guid EventId { get; }
        DateTime TimeStamp { get; }
    }
}
