using System;
using Newtonsoft.Json;

using Reshape.BusinessManagementService.API.Application.Commands;
using Reshape.Common.EventBus.Events.Contracts;

namespace Reshape.BusinessManagementService.API.Application.IntegrationEvents.Events
{
    public class BusinessTierCreatedEvent : BusinessTierCreated
    {
        public Guid Id { get; }

        public string Name { get; }

        public string Description { get; }

        public decimal Price { get; }

        public Guid EventId { get; }

        public DateTime TimeStamp { get; }

        public BusinessTierCreatedEvent(BusinessTierDTO businessTierDTO)
        {
            EventId = Guid.NewGuid();
            TimeStamp = DateTime.UtcNow;
            Id = businessTierDTO.Id;
            Name = businessTierDTO.Name;
            Description = businessTierDTO.Description;
            Price = businessTierDTO.Price;
        }

        [JsonConstructor]
        public BusinessTierCreatedEvent(Guid eventId, DateTime timeStamp, Guid id, string name, string description, decimal price)
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