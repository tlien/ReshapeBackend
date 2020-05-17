using System;
using BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;

namespace BusinessManagementService.Domain.AggregatesModel.AnalysisProfilePackageAggregate
{
    public class AnalysisProfileAnalysisProfilePackage
    {
        public Guid AnalysisProfileId { get; set; }
        public AnalysisProfile AnalysisProfile { get; set; }
        public Guid AnalysisProfilePackageId { get; set; }
        public AnalysisProfilePackage AnalysisProfilePackage { get; set;}
    }
}