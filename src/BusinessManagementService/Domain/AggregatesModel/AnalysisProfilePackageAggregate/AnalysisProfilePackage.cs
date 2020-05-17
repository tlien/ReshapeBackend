using System.Collections.Generic;
using System.Linq;
using BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using Common.SeedWork;

namespace BusinessManagementService.Domain.AggregatesModel.AnalysisProfilePackageAggregate
{
    public class AnalysisProfilePackage : Entity, IAggregateRoot
    {
        private string _name;
        private string _description;
        private decimal _price;


        private readonly List<AnalysisProfileAnalysisProfilePackage> _analysisProfileAnalysisProfilePackages;
        public IReadOnlyCollection<AnalysisProfileAnalysisProfilePackage> AnalysisProfileAnalysisProfilePackages => _analysisProfileAnalysisProfilePackages;
        public IReadOnlyCollection<AnalysisProfile> GetAnalysisProfiles => _analysisProfileAnalysisProfilePackages.Select(a => a.AnalysisProfile).ToList();

        public AnalysisProfilePackage()
        {
            _analysisProfileAnalysisProfilePackages = new List<AnalysisProfileAnalysisProfilePackage>();
        }

        public AnalysisProfilePackage(string name, string description, decimal price) : this()
        {
            _name = name;
            _description = description;
            _price = price;
        }

        public void AddAnalysisProfile(AnalysisProfileAnalysisProfilePackage analysisProfile)
        {
            var existingAnalysisProfile = _analysisProfileAnalysisProfilePackages.FirstOrDefault(a => a.AnalysisProfileId == analysisProfile.AnalysisProfileId);

            if(existingAnalysisProfile == null)
                _analysisProfileAnalysisProfilePackages.Add(analysisProfile);
        }

        public void RemoveAnalysisProfile(AnalysisProfileAnalysisProfilePackage analysisProfile)
        {
            _analysisProfileAnalysisProfilePackages.Remove(analysisProfile);
        }

        public void SetPrice(decimal price)
        {
            _price = price;
        }
    }
}