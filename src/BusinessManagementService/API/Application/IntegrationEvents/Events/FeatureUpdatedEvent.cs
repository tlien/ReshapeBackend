using System;
using Newtonsoft.Json;

using Reshape.BusinessManagementService.API.Application.Commands;
using Reshape.Common.EventBus.Events.Contracts;

namespace Reshape.BusinessManagementService.API.Application.IntegrationEvents.Events
{
    /// <summary>
    /// Integration event to inform subscribers that a specific <c>Feature</c> has been updated.
    /// </summary>
    public class FeatureUpdatedEvent : FeatureUpdated
    {
        public Guid Id { get; }

        public string Name { get; }

        public string Description { get; }

        public decimal Price { get; }

        public Guid EventId { get; }

        public DateTime TimeStamp { get; }
        public FeatureUpdatedEvent(FeatureDTO featureDTO)
        {
            EventId = Guid.NewGuid();
            TimeStamp = DateTime.UtcNow;
            Id = featureDTO.Id;
            Name = featureDTO.Name;
            Description = featureDTO.Description;
            Price = featureDTO.Price;
        }

        [JsonConstructor]
        public FeatureUpdatedEvent(Guid eventId, DateTime timeStamp, Guid id, string name, string description, decimal price)
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