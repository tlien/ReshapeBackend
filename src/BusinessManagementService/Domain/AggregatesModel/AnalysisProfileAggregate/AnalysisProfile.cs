using System;
using System.Collections.Generic;
using BusinessManagementService.Domain.AggregatesModel.FeatureAggregate;
using Common.SeedWork;

namespace BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate {
    public class AnalysisProfile : Entity, IAggregateRoot {
        private string _name;
        private string _description;
        private string _fileName;
        private readonly Feature[] _requiredFeatures;
        public IReadOnlyCollection<Feature> RequiredFeatures => _requiredFeatures;

        public AnalysisProfile(string name, string description, string fileName, Feature[] requiredFeatures) {
            _name = name;
            _description = description;
            _fileName = fileName;
            _requiredFeatures = requiredFeatures;
        }
    }

}