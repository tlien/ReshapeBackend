using System;
using Reshape.Common.SeedWork;

namespace Reshape.AccountService.Domain.AggregatesModel.AccountAggregate
{
    public class BusinessTier : Entity
    {
        private readonly string _name;

        protected BusinessTier() { }

        public BusinessTier(Guid id, string name)
        {
            base.Id = id;
            _name = name;
        }

        public string GetName() => _name;
    }
}