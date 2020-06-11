using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Reshape.Common.SeedWork;

namespace Reshape.AccountService.Domain.AggregatesModel.AccountAggregate
{
    public interface IAccountRepository : IRepository<Account>
    {
        Account Add(Account account);
        void Update(Account account);
        Task<Account> GetAsync(Guid accountId);

        Task<List<Feature>> GetFeaturesAsync(List<Guid> featureIds);
        Task<Feature> AddFeature(Feature feature);

        Task<BusinessTier> GetBusinessTierAsync(Guid businessTierId);
        Task<BusinessTier> AddBusinessTier(BusinessTier businessTier);

        Task<List<AnalysisProfile>> GetAnalysisProfilesAsync(List<Guid> analysisProfileIds);
        Task<AnalysisProfile> AddAnalysisProfile(AnalysisProfile analysisProfile);
    }
}