using System;
using System.Collections.Generic;
using Common.SeedWork;
using AccountService.Infrastructure;

namespace AccountService.Domain.AggregatesModel.AccountAggregate {
    public class Account : Entity, IAggregateRoot {
        public Address address { get; private set; }
        public ContactDetails contactDetails { get; private set; }

        private BusinessTier _businessTier;
        public BusinessTier GetBusinessTier => _businessTier;

        // private readonly List<Feature> _features;
        // public IReadOnlyCollection<Feature> features => _features;
        
        private readonly List<AccountFeature> _features;
        public IReadOnlyCollection<AccountFeature> features => _features;

        public static Account NewAccount()
        {
            var Account = new Account();
            return Account;
        }

        // protected Account() {
        //     _features = new List<Feature>();
        //     _businessTier = new BusinessTier("free");
        // }

        public void SetAddress(Address newAddress) {
            address = newAddress;
        }

        public void SetContactDetails(ContactDetails newContactDetails) {
            contactDetails = newContactDetails;
        }

        // public void AddFeatures(Feature feature) {
        //     _features.Add(feature);
        // }

        // public void RemoveFeatures(Feature feature) {
        //     _features.Remove(feature);
        // }

        public void SetBusinessTier(BusinessTier businessTier) {
            _businessTier = businessTier;
        }
    }
}