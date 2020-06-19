using System;

using Reshape.Common.EventBus.Events;

namespace Reshape.Common.EventBus
{
    public interface IEventTracker
    {
        Type GetEventTypeByName(string eventName);
        void AddEventType<T>() where T : IIntegrationEvent;
    }
}