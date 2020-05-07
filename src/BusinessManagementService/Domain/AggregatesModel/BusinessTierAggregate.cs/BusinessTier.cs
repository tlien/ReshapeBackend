using Common.SeedWork;

namespace BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate {
    public class BusinessTier : Entity, IAggregateRoot {
        private string _name;

        public BusinessTier(string name) {
            _name = name;
        }
    }
}