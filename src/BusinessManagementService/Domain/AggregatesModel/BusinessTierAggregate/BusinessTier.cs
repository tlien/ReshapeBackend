using Common.SeedWork;

namespace BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate {
    public class BusinessTier : Entity, IAggregateRoot 
    {
        private string _name;
        public string GetName => _name;
        private string _description;
        private decimal _price;
        public BusinessTier(string name, string description, decimal price) {
            _name = name;
            _description = description;
            _price = price;
        }
    }
}