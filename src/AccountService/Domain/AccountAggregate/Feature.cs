using System.Collections.Generic;
using AccountService.Infrastructure;
using Common.SeedWork;

namespace AccountService.Domain.AggregatesModel.AccountAggregate
{
    public class Feature : Entity
    {
        private string _name;
        private string _description;

        protected Feature() { }

        public Feature(string name, string description)
        {
            _name = name;
            _description = description;
        }

        public string GetName() => _name;
        public string GetDescription() => _description;
    }
}