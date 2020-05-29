using System;

using Reshape.Common.SeedWork;

namespace Reshape.AccountService.Domain.AggregatesModel.AccountAggregate
{
    public class Feature : Entity
    {
        private readonly string _name;
        private readonly string _description;

        protected Feature() { }

        public Feature(Guid id, string name, string description)
        {
            base.Id = id;
            _name = name;
            _description = description;
        }

        public string GetName() => _name;
        public string GetDescription() => _description;
    }
}