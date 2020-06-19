using Reshape.Common.SeedWork;

namespace Reshape.BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate
{
    public class BusinessTier : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public BusinessTier(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }
    }
}