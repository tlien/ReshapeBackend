using System;
using System.Collections.Generic;
using System.Linq;
using Reshape.Common.EventBus.Events;

namespace Reshape.Common.EventBus
{
    public class EventTracker : IEventTracker
    {
        private readonly List<Type> _eventTypes;

        public EventTracker()
        {
            _eventTypes = new List<Type>();
        }

        public void AddEventType<T>() where T : IntegrationEvent
        {
            if(!_eventTypes.Contains(typeof(T)))
            {
                _eventTypes.Add(typeof(T));
            }
        }

        public Type GetEventTypeByName(string eventName) =>  _eventTypes.SingleOrDefault(e => e.Name == eventName);
    }
}