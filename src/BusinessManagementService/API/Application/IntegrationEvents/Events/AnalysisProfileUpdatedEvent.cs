using System;
using Newtonsoft.Json;

using Reshape.Common.EventBus.Events.Contracts;
using Reshape.BusinessManagementService.API.Application.Commands;

namespace Reshape.BusinessManagementService.API.Application.IntegrationEvents.Events
{
    public class AnalysisProfileUpdatedEvent : AnalysisProfileUpdated
    {
        public Guid Id { get; }

        public string Name { get; }

        public string Description { get; }

        public decimal Price { get; }

        public Guid EventId { get; }

        public DateTime TimeStamp { get; }

        public AnalysisProfileUpdatedEvent(AnalysisProfileDTO analysisProfileDTO)
        {
            EventId = Guid.NewGuid();
            TimeStamp = DateTime.UtcNow;
            Id = analysisProfileDTO.Id;
            Name = analysisProfileDTO.Name;
            Description = analysisProfileDTO.Description;
            Price = analysisProfileDTO.Price;
        }

        [JsonConstructor]
        public AnalysisProfileUpdatedEvent(Guid eventId, DateTime timeStamp, Guid id, string name, string description, decimal price)
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