using Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using Reshape.Common.EventBus.Events;

namespace Reshape.BusinessManagementService.API.Application.IntegrationEvents.Events
{
    public class NewAnalysisProfileIntegrationEvent : IntegrationEvent
    {
        public AnalysisProfile AnalysisProfile { get; set; }
        
        public NewAnalysisProfileIntegrationEvent(AnalysisProfile analysisProfile)
        {
            AnalysisProfile = analysisProfile;
        }
    }
}