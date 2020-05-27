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
        private readonly List<AnalysisProfile> _analysisProfiles;
        public IReadOnlyCollection<AnalysisProfile> AnalysisProfiles => _analysisProfiles;

        public static Account NewAccount()
        {
            var Account = new Account();
            return Account;
        }

        protected Account()
        {
            _isActive = true;
            _features = new List<Feature>();
            _analysisProfiles = new List<AnalysisProfile>();
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

        public void AddAnalysisProfile(AnalysisProfile analysisProfile)
        {
            if (!_analysisProfiles.Exists(ap => ap.Id == analysisProfile.Id))
            {
                _analysisProfiles.Add(analysisProfile);
            }

            // TODO: Maybe log that an attempt to add an existing analysisProfile happened?
        }

        public void RemoveAnalysisProfile(AnalysisProfile analysisProfile)
        {
            _analysisProfiles.Remove(analysisProfile);
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