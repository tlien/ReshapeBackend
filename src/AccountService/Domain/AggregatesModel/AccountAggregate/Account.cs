using System.Collections.Generic;
using System.Linq;

using Reshape.Common.SeedWork;
using Reshape.AccountService.Infrastructure;

namespace Reshape.AccountService.Domain.AggregatesModel.AccountAggregate
{
    public class Account : Entity, IAggregateRoot
    {
        public bool IsActive { get; private set; }

        // Value objects
        public Address Address { get; private set; }
        public ContactDetails ContactDetails { get; private set; }

        // Entities
        public BusinessTier BusinessTier { get; private set; } // auto prop

        private readonly List<AccountFeature> _accountFeatures;
        public IReadOnlyCollection<AccountFeature> AccountFeatures => _accountFeatures;
        public IReadOnlyCollection<Feature> Features => _accountFeatures.Select(af => af.Feature).ToList();

        private readonly List<AccountAnalysisProfile> _accountAnalysisProfiles;
        public IReadOnlyCollection<AccountAnalysisProfile> AccountAnalysisProfiles => _accountAnalysisProfiles;
        public IReadOnlyCollection<AnalysisProfile> AnalysisProfiles => _accountAnalysisProfiles.Select(aap => aap.AnalysisProfile).ToList();

        public static Account NewAccount()
        {
            var Account = new Account();
            return Account;
        }

        protected Account()
        {
            IsActive = true;
            _accountFeatures = new List<AccountFeature>();
            _accountAnalysisProfiles = new List<AccountAnalysisProfile>();
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
            if (!_accountFeatures.Exists(af => af.FeatureId == feature.Id))
            {
                _accountFeatures.Add(new AccountFeature()
                {
                    FeatureId = feature.Id
                });
            }
            // TODO: Maybe log that an attempt to add an existing analysisProfile happened?
        }

        public void RemoveFeatures(Feature feature)
        {
            var itemToRemove = _accountFeatures.Find(af => af.FeatureId == feature.Id);
            if (itemToRemove != null)
            {
                _accountFeatures.Remove(itemToRemove);
            }
        }

        public void AddAnalysisProfile(AnalysisProfile analysisProfile)
        {
            if (!_accountAnalysisProfiles.Exists(aap => aap.AnalysisProfileId == analysisProfile.Id))
            {
                _accountAnalysisProfiles.Add(new AccountAnalysisProfile()
                {
                    AnalysisProfileId = analysisProfile.Id
                });
            }
            // TODO: Maybe log that an attempt to add an existing analysisProfile happened?
        }

        public void RemoveAnalysisProfile(AnalysisProfile analysisProfile)
        {
            var itemToRemove = _accountAnalysisProfiles.Find(ap => ap.AnalysisProfileId == analysisProfile.Id);
            if (itemToRemove != null)
            {
                _accountAnalysisProfiles.Remove(itemToRemove);
            }
        }

        public void SetBusinessTier(BusinessTier businessTier)
        {
            if (BusinessTier?.Id != businessTier.Id)
                BusinessTier = businessTier;

            // TODO: What if this is already the current businessTier?
        }

        public void SetAccountActive()
        {
            IsActive = false;
        }

        public void SetAccountInactive()
        {
            IsActive = true;
        }
    }
}