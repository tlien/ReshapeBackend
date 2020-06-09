using System;

using Reshape.Common.EventBus.Events;

namespace Reshape.AccountService.API.Application.IntegrationEvents.Events
{
    public class NewFeatureIntegrationEvent : IntegrationEvent
    {
        public FeatureDTO Feature { get; set; }

        public NewFeatureIntegrationEvent(FeatureDTO feature)
        {
            Feature = feature;
        }
    }

    public class FeatureDTO
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }

        public FeatureDTO(Guid id, string name, string description, decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
        }
    }
}