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

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public AccountRepository(AccountContext ctx)
        {
            _context = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }

        public Account Add(Account account)
        {
            return _context.Accounts.Add(account).Entity;
        }

        public void Update(Account account)
        {
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

            // TODO: Figure out if these actually need to be loaded at this point
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

        public async Task<List<Feature>> GetFeaturesAsync(List<Guid> featureIds)
        {
            var features = await _context
                                    .Features
                                    .Where(f => featureIds.Contains(f.Id))
                                    .ToListAsync();
            return features;
        }

        public async void AddFeature(Feature feature)
        {
            _context.Features.Add(feature);
            await _context.SaveChangesAsync();
        }

        public async Task<BusinessTier> GetBusinessTierAsync(Guid businessTierId)
        {
            var businessTier = await _context
                                    .BusinessTiers
                                    .FirstOrDefaultAsync(bt => bt.Id == businessTierId);
            return businessTier;
        }

        public async void AddBusinessTier(BusinessTier businessTier)
        {
            _context.BusinessTiers.Add(businessTier);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AnalysisProfile>> GetAnalysisProfilesAsync(List<Guid> analysisProfileIds)
        {
            var analysisProfiles = await _context
                                    .AnalysisProfiles
                                    .Where(ap => analysisProfileIds.Contains(ap.Id))
                                    .ToListAsync();
            return analysisProfiles;
        }

        public async void AddAnalysisProfile(AnalysisProfile analysisProfile)
        {
            _context.AnalysisProfiles.Add(analysisProfile);
            await _context.SaveChangesAsync();
        }
    }
}