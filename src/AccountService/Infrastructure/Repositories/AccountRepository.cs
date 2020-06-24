using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Reshape.Common.SeedWork;
using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;

namespace Reshape.AccountService.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for the <c>Account</c> domain aggregate.
    /// Implements <c>IAccountRepository</c>
    /// </summary>
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountContext _context;

        /// <summary>
        /// Gets underlying DbContext (DbContexts implement the Unit of Work pattern).
        /// </summary>
        public IUnitOfWork UnitOfWork => _context;

        public AccountRepository(AccountContext ctx)
        {
            _context = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }

        #region Account
        /// <summary>
        /// Adds an <c>Account</c> to the <c>AccountContext</c> in-memory
        /// </summary>
        /// <param name="account"></param>
        /// <returns>The added <c>Account</c> entity</returns>
        public Account Add(Account account)
        {
            return _context.Accounts.Add(account).Entity;
        }

        /// <summary>
        /// Sets the change tracking <c>State</c>
        /// of an <c>Account</c> to <c>EntityState.Modified</c>.
        /// This allows changes that have been made to the tracked <c>Account</c> to be
        /// committed when changes are saved to the database context.
        /// </summary>
        /// <param name="account"></param>
        public void Update(Account account)
        {
            // Only track updates to the Account entity itself.
            _context.Entry(account).State = EntityState.Modified;
        }

        /// <summary>
        /// Gets a single <c>Account</c> by id.
        /// If the <c>Account</c> does not exist in the database,
        /// an additional in-memory look-up will take place.
        /// </summary>
        /// <param name="accountId">The <c>Account.Id</c></param>
        /// <returns>A Task that returns the retrieved <c>Account</c> when awaited.</returns>
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
        /// <summary>
        /// Gets a list of <c>Features</c> by their ids.
        /// </summary>
        /// <returns>A Task that returns a list of <c>Features</c> when awaited.</returns>
        public async Task<List<Feature>> GetFeaturesAsync(List<Guid> featureIds)
        {
            var features = await _context
                                    .Features
                                    .Where(f => featureIds.Contains(f.Id))
                                    .ToListAsync();
            return features;
        }

        /// <summary>
        /// Adds an <c>Feature</c> to the <c>AccountContext</c> in-memory
        /// </summary>
        /// <param name="feature"></param>
        /// <returns>The added <c>Feature</c> entity</returns>
        public async Task<Feature> AddFeature(Feature feature)
        {
            var res = _context.Features.Add(feature).Entity;
            await _context.SaveChangesAsync();
            return res;
        }

        /// <summary>
        /// Sets the change tracking <c>State</c>
        /// of an <c>Feature</c> to <c>EntityState.Modified</c>.
        /// This allows changes that have been made to the tracked <c>Feature</c> to be
        /// committed when changes are saved to the database context.
        /// </summary>
        /// <param name="feature"></param>
        public async Task<Feature> UpdateFeature(Feature feature)
        {
            var res = _context.Features.Update(feature).Entity;
            await _context.SaveChangesAsync();
            return res;
        }
        #endregion

        #region BusinessTier
        /// <summary>
        /// Gets a single <c>BusinessTier</c> by id.
        /// </summary>
        /// <returns>A Task that returns a <c>BusinessTier</c> when awaited.</returns>
        public async Task<BusinessTier> GetBusinessTierAsync(Guid businessTierId)
        {
            var businessTier = await _context
                                        .BusinessTiers
                                        .FirstOrDefaultAsync(bt => bt.Id == businessTierId);
            return businessTier;
        }

        /// <summary>
        /// Adds an <c>BusinessTier</c> to the <c>AccountContext</c> in-memory
        /// </summary>
        /// <param name="businessTier"></param>
        /// <returns>The added <c>BusinessTier</c> entity</returns>
        public async Task<BusinessTier> AddBusinessTier(BusinessTier businessTier)
        {
            var res = _context.BusinessTiers.Add(businessTier).Entity;
            await _context.SaveChangesAsync();
            return res;
        }

        /// <summary>
        /// Sets the change tracking <c>State</c>
        /// of an <c>BusinessTier</c> to <c>EntityState.Modified</c>.
        /// This allows changes that have been made to the tracked <c>BusinessTier</c> to be
        /// committed when changes are saved to the database context.
        /// </summary>
        /// <param name="businessTier"></param>
        public async Task<BusinessTier> UpdateBusinessTier(BusinessTier businessTier)
        {
            var res = _context.BusinessTiers.Update(businessTier).Entity;
            await _context.SaveChangesAsync();
            return res;
        }
        #endregion

        #region AnalysisProfile
        /// <summary>
        /// Gets a list of <c>AnalysisProfiles</c> by their ids.
        /// </summary>
        /// <returns>A Task that returns a list of <c>AnalysisProfiles</c> when awaited.</returns>
        public async Task<List<AnalysisProfile>> GetAnalysisProfilesAsync(List<Guid> analysisProfileIds)
        {
            var analysisProfiles = await _context
                                            .AnalysisProfiles
                                            .Where(ap => analysisProfileIds.Contains(ap.Id))
                                            .ToListAsync();
            return analysisProfiles;
        }

        /// <summary>
        /// Adds an <c>AnalysisProfile</c> to the <c>AccountContext</c> in-memory
        /// </summary>
        /// <param name="analysisProfile"></param>
        /// <returns>The added <c>AnalysisProfile</c> entity</returns>
        public async Task<AnalysisProfile> AddAnalysisProfile(AnalysisProfile analysisProfile)
        {
            var res = _context.AnalysisProfiles.Add(analysisProfile).Entity;
            await _context.SaveChangesAsync();
            return res;
        }

        /// <summary>
        /// Sets the change tracking <c>State</c>
        /// of an <c>AnalysisProfile</c> to <c>EntityState.Modified</c>.
        /// This allows changes that have been made to the tracked <c>AnalysisProfile</c> to be
        /// committed when changes are saved to the database context.
        /// </summary>
        /// <param name="analysisProfile"></param>
        public async Task<AnalysisProfile> UpdateAnalysisProfile(AnalysisProfile analysisProfile)
        {
            var res = _context.AnalysisProfiles.Update(analysisProfile).Entity;
            await _context.SaveChangesAsync();
            return res;
        }
        #endregion
    }
}