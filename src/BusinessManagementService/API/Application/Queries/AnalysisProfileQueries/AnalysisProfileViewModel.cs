using System;
using System.Collections.Generic;

namespace BusinessManagementService.API.Application.Queries.AnalysisProfileQueries 
{
    public class AnalysisProfileViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public List<AnalysisProfileRequiredFeatureViewModel> RequiredFeatures { get; set; }
    }

    public class AnalysisProfileRequiredFeatureViewModel 
    {
        public Guid AnalysisProfileID { get; set; }
        public Guid FeatureID { get; set; }
    }
}