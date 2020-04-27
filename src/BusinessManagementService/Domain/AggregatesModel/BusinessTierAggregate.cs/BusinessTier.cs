using Common.SeedWork;

namespace BusinessManagementService.Domain.AggregatesModel {
    public class BusinessTier : Entity, IAggregateRoot {
        public string Name { get; private set; }
    }
}