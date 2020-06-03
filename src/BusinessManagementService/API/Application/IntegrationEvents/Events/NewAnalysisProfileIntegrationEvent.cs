using Reshape.Common.EventBus.Events;
using static Reshape.BusinessManagementService.API.Application.Commands.CreateAnalysisProfileCommandHandler;

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