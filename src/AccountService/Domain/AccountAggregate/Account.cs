using System;
using System.Collections.Generic;
using Reshape.Common.SeedWork;
using Reshape.AccountService.Infrastructure;

namespace Reshape.AccountService.Domain.AggregatesModel.AccountAggregate
{
    public class Account : Entity, IAggregateRoot
    {
        private bool _isActive;
        public bool GetIsActive => _isActive;

        public Address address { get; private set; }
        public ContactDetails contactDetails { get; private set; }

        private BusinessTier _businessTier;
        public BusinessTier GetBusinessTier => _businessTier;

        private readonly List<AccountFeature> _features;
        public IReadOnlyCollection<AccountFeature> features => _features;

        public static Account NewAccount()
        {
            var Account = new Account();
            return Account;
        }

        protected Account()
        {
            _isActive = true;
            _features = new List<AccountFeature>();
            _businessTier = new BusinessTier("free");
        }

        public void SetAddress(Address newAddress)
        {
            address = newAddress;
        }

        public void SetContactDetails(ContactDetails newContactDetails)
        {
            contactDetails = newContactDetails;
        }

        public void AddFeatures(AccountFeature feature)
        {
            _features.Add(feature);
        }

        public void RemoveFeatures(AccountFeature feature)
        {
            _features.Remove(feature);
        }

        public void SetBusinessTier(BusinessTier businessTier)
        {
            _businessTier = businessTier;
        }

        public void SetAccountActive()
        {
            _isActive = false;
        }

        public void SetAccountInactive()
        {
            _isActive = true;
        }
    }
}