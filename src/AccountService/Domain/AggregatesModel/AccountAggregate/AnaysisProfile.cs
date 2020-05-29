using System;

using Reshape.Common.SeedWork;

namespace Reshape.AccountService.Domain.AggregatesModel.AccountAggregate
{
    public class AnalysisProfile : Entity
    {
        private readonly string _name;
        private readonly string _description;
        private readonly decimal _price;

        protected AnalysisProfile() { }

        public AnalysisProfile(Guid id, string name, string description, decimal price)
        {
            base.Id = id;
            _name = name;
            _description = description;
            _price = price;
        }

        public string GetName() => _name;
        public string GetDescription() => _description;
        public decimal GetPrice() => _price;
    }
}