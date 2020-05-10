using System;
using System.Collections.Generic;
using System.Linq;
using BusinessManagementService.Domain.AggregatesModel.FeatureAggregate;
using Common.SeedWork;

namespace BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate {
    public class AnalysisProfile : Entity, IAggregateRoot {
        private string _name;
        public string GetName => _name;
        private string _description;
        public string GetDescription => _description;
        private string _fileName;
        public string GetFileName => _fileName;
        private readonly List<AnalysisProfileRequiredFeature> _requiredFeatures;
        public IReadOnlyCollection<AnalysisProfileRequiredFeature> RequiredFeatures => _requiredFeatures;

        protected AnalysisProfile()
        {
            _requiredFeatures = new List<AnalysisProfileRequiredFeature>();
        }
        public AnalysisProfile(string name, string description, string fileName) : this()
        {
            _name = name;
            _description = description;
            _fileName = fileName;
        }

        // public void SetRequiredFeatures(List<AnalysisProfileRequiredFeature> incomingFeatures) {
        //     List<Guid> incomingFeatureIDs = incomingFeatures.Select(f => f.FeatureID).ToList();
        //     List<Guid> currentFeatureIDs = _requiredFeatures.Select(rf => rf.FeatureID).ToList();

        //     foreach(var feature in _requiredFeatures ) {
        //         if(!incomingFeatureIDs.Contains(feature.FeatureID)) {
        //             _requiredFeatures.Remove(feature);
        //         }
        //     }

        //     foreach(var feature in incomingFeatures) {
        //         if(!currentFeatureIDs.Contains(feature.FeatureID)) {
        //             _requiredFeatures.Add(feature);
        //         }
        //     }
        // }

        public void AddRequiredFeature(AnalysisProfileRequiredFeature requiredFeature) {
            _requiredFeatures.Add(requiredFeature);
        }
    }

}