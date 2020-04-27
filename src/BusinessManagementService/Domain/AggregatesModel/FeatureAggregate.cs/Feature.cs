using Common.SeedWork;

namespace BusinessManagementService.Domain.AggregatesModel.FeatureAggregate {
    public class Feature : Entity, IAggregateRoot {
        private string _name;
        private string _description;

        public Feature (string name, string description) {
            _name = name;
            _description = description;
        }
    }
}