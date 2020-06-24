using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

using Reshape.Common.EventBus.Events;

namespace Reshape.Common.EventBus
{
    public class IntegrationEventLogEntry
    {
        private IntegrationEventLogEntry() { }
        public IntegrationEventLogEntry(IIntegrationEvent @event, Guid transactionId)
        {
            EventId = @event.EventId;
            TimeStamp = @event.TimeStamp;
            EventTypeName = @event.GetType().FullName;
            Content = JsonConvert.SerializeObject(@event);
            State = EventStateEnum.NotPublished;
            TimesSent = 0;
            TransactionId = transactionId.ToString();
        }
        public Guid EventId { get; private set; }
        public string EventTypeName { get; private set; }
        public EventStateEnum State { get; set; }
        public int TimesSent { get; set; }
        public DateTime TimeStamp { get; private set; }
        public string Content { get; private set; }
        public string TransactionId { get; private set; }

        [NotMapped]
        public Type EventType { get; private set; }
        [NotMapped]
        public string EventTypeShortName => EventTypeName.Split('.')?.Last();
        [NotMapped]
        public IIntegrationEvent IntegrationEvent { get; private set; }

        public IntegrationEventLogEntry DeserializeJsonContent(Type type)
        {
            // TODO: Use System.Text.Json when it has matured
            // The System.Text.Json workaround is currently rather cumbersome: https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-migrate-from-newtonsoft-how-to#deserialize-to-immutable-classes-and-structs
            IntegrationEvent = JsonConvert.DeserializeObject(Content, type) as IIntegrationEvent;
            EventType = type;
            return this;
        }
    }
}
