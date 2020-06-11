using System;

using Newtonsoft.Json;
using Reshape.BusinessManagementService.API.Application.Commands;
using Reshape.Common.EventBus.Events.Contracts;

namespace Reshape.BusinessManagementService.API.Application.IntegrationEvents.Events
{
    public class FeatureCreatedEvent : FeatureCreated
    {
        public Guid Id { get; }

        public string Name { get; }

        public string Description { get; }

        public decimal Price { get; }

        public Guid EventId { get; }

        public DateTime TimeStamp { get; }
        public FeatureCreatedEvent(FeatureDTO featureDTO)
        {
            EventId = Guid.NewGuid();
            TimeStamp = DateTime.UtcNow;
            Id = featureDTO.Id;
            Name = featureDTO.Name;
            Description = featureDTO.Description;
            Price = featureDTO.Price;
        }

        [JsonConstructor]
        public FeatureCreatedEvent(Guid eventId, DateTime timeStamp, Guid id, string name, string description, decimal price)
        {
            EventId = eventId;
            TimeStamp = timeStamp;
            Id = id;
            Name = name;
            Description = description;
            Price = price;
        }
    }
}