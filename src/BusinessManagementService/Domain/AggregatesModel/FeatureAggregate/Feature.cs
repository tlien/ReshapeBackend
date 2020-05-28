using Reshape.Common.SeedWork;

namespace Reshape.BusinessManagementService.Domain.AggregatesModel.FeatureAggregate {
    public class Feature : Entity, IAggregateRoot 
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }

        public Feature (string name, string description, decimal price) {
            Name = name;
            Description = description;
            Price = price;
        }
    }
}