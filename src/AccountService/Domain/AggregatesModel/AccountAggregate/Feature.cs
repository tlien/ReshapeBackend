using System;

using Reshape.Common.SeedWork;

namespace Reshape.AccountService.Domain.AggregatesModel.AccountAggregate
{
    public class Feature : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }

        protected Feature() { }

        public Feature(Guid id, string name, string description, decimal price)
        {
            base.Id = id;
            Name = name;
            Description = description;
            Price = price;
        }
    }
}