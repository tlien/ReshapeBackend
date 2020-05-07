using System;
using BusinessManagementService.Domain.AggregatesModel.FeatureAggregate;

namespace BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate {
    public class AnalysisProfileRequiredFeature
    {
        public Guid AnalysisProfileID { get; set; }
        public AnalysisProfile AnalysisProfile { get; set;}
        public Guid FeatureID { get; set; }
        public Feature Feature { get; set; }
    }
}