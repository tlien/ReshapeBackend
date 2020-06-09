using System;

using Reshape.Common.EventBus.Events;

namespace Reshape.AccountService.API.Application.IntegrationEvents.Events
{
    public class NewAnalysisProfileIntegrationEvent : IntegrationEvent
    {
        public AnalysisProfileDTO AnalysisProfile { get; set; }

        public NewAnalysisProfileIntegrationEvent(AnalysisProfileDTO analysisProfile)
        {
            AnalysisProfile = analysisProfile;
        }
    }

    public class AnalysisProfileDTO
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }

        public AnalysisProfileDTO(Guid id, string name, string description, decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
        }
    }
}