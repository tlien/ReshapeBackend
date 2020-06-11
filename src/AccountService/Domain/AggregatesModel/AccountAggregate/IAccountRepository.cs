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
        void AddFeature(Feature feature);
        Task<BusinessTier> GetBusinessTierAsync(Guid businessTierId);
        void AddBusinessTier(BusinessTier businessTier);
        Task<List<AnalysisProfile>> GetAnalysisProfilesAsync(List<Guid> analysisProfileIds);
        void AddAnalysisProfile(AnalysisProfile analysisProfile);
    }
}