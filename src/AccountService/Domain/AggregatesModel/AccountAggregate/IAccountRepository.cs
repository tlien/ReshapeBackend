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
        Task<BusinessTier> GetBusinessTierAsync(Guid businessTierId);
        Task<List<AnalysisProfile>> GetAnalysisProfilesAsync(List<Guid> analysisProfileIds);
    }
}