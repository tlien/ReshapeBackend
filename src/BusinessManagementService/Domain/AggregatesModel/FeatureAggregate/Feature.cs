using System;

using Reshape.Common.SeedWork;

namespace Reshape.BusinessManagementService.Domain.AggregatesModel.FeatureAggregate
{
    public class Feature : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }

        public Feature(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        // ctor used for seeding don't put this in production.
        public Feature(Guid id, string name, string description, decimal price) : this(name, description, price)
        {
            base.Id = id;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void SetPrice(decimal price)
        {
            Price = price;
        }
    }
}