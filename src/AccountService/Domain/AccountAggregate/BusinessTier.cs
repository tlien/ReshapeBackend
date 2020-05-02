using Common.SeedWork;

namespace AccountService.Domain.AggregatesModel.AccountAggregate {
    public class BusinessTier : Entity {
        private string _name;

        protected BusinessTier() {}

        public BusinessTier(string name) {
            _name = name;
        }

        public string GetName() => _name;
    }
}