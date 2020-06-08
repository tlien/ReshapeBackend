using Reshape.Common.EventBus.Events;
using Reshape.BusinessManagementService.API.Application.Commands;

namespace Reshape.BusinessManagementService.API.Application.IntegrationEvents.Events
{
    public class NewBusinessTierIntegrationEvent : IntegrationEvent
    {
        public BusinessTierDTO BusinessTier { get; set; }

        public NewBusinessTierIntegrationEvent(BusinessTierDTO businessTier)
        {
            BusinessTier = businessTier;
        }
    }
}