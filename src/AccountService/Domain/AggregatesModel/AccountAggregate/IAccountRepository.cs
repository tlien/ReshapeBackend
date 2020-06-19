using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Reshape.Common.SeedWork;

namespace Reshape.AccountService.Domain.AggregatesModel.AccountAggregate
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetAsync(Guid accountId);
        Account Add(Account account);
        void Update(Account account);

        Task<List<Feature>> GetFeaturesAsync(List<Guid> featureIds);
        Task<Feature> AddFeature(Feature feature);
        Task<Feature> UpdateFeature(Feature feature);

        Task<BusinessTier> GetBusinessTierAsync(Guid businessTierId);
        Task<BusinessTier> AddBusinessTier(BusinessTier businessTier);
        Task<BusinessTier> UpdateBusinessTier(BusinessTier businessTier);

        Task<List<AnalysisProfile>> GetAnalysisProfilesAsync(List<Guid> analysisProfileIds);
        Task<AnalysisProfile> AddAnalysisProfile(AnalysisProfile analysisProfile);
        Task<AnalysisProfile> UpdateAnalysisProfile(AnalysisProfile analysisProfile);
    }
}