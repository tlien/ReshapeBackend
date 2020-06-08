using Reshape.BusinessManagementService.API.Application.Commands;
using Reshape.Common.EventBus.Events;

namespace Reshape.BusinessManagementService.API.Application.IntegrationEvents.Events
{
    public class NewAnalysisProfileIntegrationEvent : IntegrationEvent
    {
        public AnalysisProfileDTO AnalysisProfile { get; set; }

        public NewAnalysisProfileIntegrationEvent(AnalysisProfileDTO analysisProfile)
        {
            AnalysisProfile = analysisProfile;
        }
    }
}