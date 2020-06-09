using System;

using Reshape.Common.EventBus.Events;

namespace Reshape.AccountService.API.Application.IntegrationEvents.Events
{
    public class NewBusinessTierIntegrationEvent : IntegrationEvent
    {
        public BusinessTierDTO BusinessTier { get; set; }

        public NewBusinessTierIntegrationEvent(BusinessTierDTO businessTier)
        {
            BusinessTier = businessTier;
        }
    }

    public class BusinessTierDTO
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }

        public BusinessTierDTO(Guid id, string name, string description, decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
        }
    }
}