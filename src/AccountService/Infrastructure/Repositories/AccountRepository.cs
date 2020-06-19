using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Reshape.Common.SeedWork;
using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public AccountRepository(AccountContext ctx)
        {
            _context = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }

        #region Account
        public Account Add(Account account)
        {
            return _context.Accounts.Add(account).Entity;
        }

        public void Update(Account account)
        {
            // Only track updates to the Account entity itself.
            _context.Entry(account).State = EntityState.Modified;
        }

        public async Task<Account> GetAsync(Guid accountId)
        {
            var account = await _context
                                    .Accounts
                                    .Include(x => x.Address)
                                    .Include(x => x.ContactDetails)
                                    .Include(x => x.BusinessTier)
                                    .Include(x => x.AccountFeatures).ThenInclude(x => x.Feature)
                                    .Include(x => x.AccountAnalysisProfiles).ThenInclude(x => x.AnalysisProfile)
                                    .FirstOrDefaultAsync(acc => acc.Id == accountId);

            // try to get account from local uncommitted entities
            if (account == null)
            {
                account = _context
                            .Accounts
                            .Local
                            .FirstOrDefault(acc => acc.Id == accountId);
            }

            // TODO: Figure out if these actually need to be loaded at this point or if they can be lazy loaded when called for
            if (account != null)
            {
                await _context.Entry(account).Reference(x => x.Address).LoadAsync();
                await _context.Entry(account).Reference(x => x.ContactDetails).LoadAsync();
                await _context.Entry(account).Reference(x => x.BusinessTier).LoadAsync();
                await _context.Entry(account).Collection(x => x.AccountFeatures).LoadAsync();
                await _context.Entry(account).Collection(x => x.AccountAnalysisProfiles).LoadAsync();
            }

            return account;
        }
        #endregion

        #region Feature
        public async Task<List<Feature>> GetFeaturesAsync(List<Guid> featureIds)
        {
            var features = await _context
                                    .Features
                                    .Where(f => featureIds.Contains(f.Id))
                                    .ToListAsync();
            return features;
        }

        public async Task<Feature> AddFeature(Feature feature)
        {
            var res = _context.Features.Add(feature).Entity;
            await _context.SaveChangesAsync();
            return res;
        }

        public async Task<Feature> UpdateFeature(Feature feature)
        {
            var res = _context.Features.Update(feature).Entity;
            await _context.SaveChangesAsync();
            return res;
        }
        #endregion

        #region BusinessTier
        public async Task<BusinessTier> GetBusinessTierAsync(Guid businessTierId)
        {
            var businessTier = await _context
                                        .BusinessTiers
                                        .FirstOrDefaultAsync(bt => bt.Id == businessTierId);
            return businessTier;
        }

        public async Task<BusinessTier> AddBusinessTier(BusinessTier businessTier)
        {
            var res = _context.BusinessTiers.Add(businessTier).Entity;
            await _context.SaveChangesAsync();
            return res;
        }

        public async Task<BusinessTier> UpdateBusinessTier(BusinessTier businessTier)
        {
            var res = _context.BusinessTiers.Update(businessTier).Entity;
            await _context.SaveChangesAsync();
            return res;
        }
        #endregion

        #region AnalysisProfile
        public async Task<List<AnalysisProfile>> GetAnalysisProfilesAsync(List<Guid> analysisProfileIds)
        {
            var analysisProfiles = await _context
                                            .AnalysisProfiles
                                            .Where(ap => analysisProfileIds.Contains(ap.Id))
                                            .ToListAsync();
            return analysisProfiles;
        }

        public async Task<AnalysisProfile> AddAnalysisProfile(AnalysisProfile analysisProfile)
        {
            var res = _context.AnalysisProfiles.Add(analysisProfile).Entity;
            await _context.SaveChangesAsync();
            return res;
        }

        public async Task<AnalysisProfile> UpdateAnalysisProfile(AnalysisProfile analysisProfile)
        {
            var res = _context.AnalysisProfiles.Update(analysisProfile).Entity;
            await _context.SaveChangesAsync();
            return res;
        }
        #endregion
    }
}