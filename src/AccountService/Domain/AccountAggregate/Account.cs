using System;
using System.Collections.Generic;
using Reshape.Common.SeedWork;

namespace Reshape.AccountService.Domain.AggregatesModel.AccountAggregate
{
    public class Account : Entity, IAggregateRoot
    {
        private bool _isActive;
        public bool GetIsActive => _isActive;

        public Address Address { get; private set; }
        public ContactDetails ContactDetails { get; private set; }

        private BusinessTier _businessTier;
        public BusinessTier BusinessTier
        {
            get
            {
                return _businessTier;
            }
            private set
            {
                _businessTier = value;
            }
        }

        private readonly List<Feature> _features;
        public IReadOnlyCollection<Feature> Features => _features;

        public static Account NewAccount()
        {
            var Account = new Account();
            return Account;
        }

        protected Account()
        {
            _isActive = true;
            _features = new List<Feature>();
        }

        public Account(Address address, ContactDetails contactDetails) : this()
        {
            Address = address;
            ContactDetails = contactDetails;
        }

        public void SetAddress(Address newAddress)
        {
            Address = newAddress;
        }

        public void SetContactDetails(ContactDetails newContactDetails)
        {
            ContactDetails = newContactDetails;
        }

        public void AddFeatures(Feature feature)
        {
            if (!_features.Exists(f => f.Id == feature.Id))
                _features.Add(feature);
        }

        public void RemoveFeatures(Feature feature)
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