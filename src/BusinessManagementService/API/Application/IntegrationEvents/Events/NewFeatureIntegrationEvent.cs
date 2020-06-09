using Reshape.BusinessManagementService.API.Application.Commands;
using Reshape.Common.EventBus.Events;

namespace Reshape.BusinessManagementService.API.Application.IntegrationEvents.Events
{
    public class NewFeatureIntegrationEvent : IntegrationEvent
    {
        public FeatureDTO Feature { get; set; }

        public NewFeatureIntegrationEvent(FeatureDTO feature)
        {
            Feature = feature;
        }
    }
}