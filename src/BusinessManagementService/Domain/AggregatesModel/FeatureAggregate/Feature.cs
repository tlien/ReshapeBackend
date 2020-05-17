using Common.SeedWork;

namespace BusinessManagementService.Domain.AggregatesModel.FeatureAggregate {
    public class Feature : Entity, IAggregateRoot 
    {
        private string _name;
        public string GetName => _name;
        private string _description;
        public string GetDescription => _description;
        private decimal _price;
        public decimal GetPrice => _price;
        public Feature (string name, string description, decimal price) {
            _name = name;
            _description = description;
            _price = price;
        }
    }
}