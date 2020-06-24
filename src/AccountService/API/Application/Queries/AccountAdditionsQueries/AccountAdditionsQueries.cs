using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper;

using Reshape.AccountService.Infrastructure;

namespace Reshape.AccountService.API.Application.Queries.AccountAdditionsQueries
{
    /// <summary>
    /// Holds database queries to get <c>Features</c>, <c>AnalysisProfiles</c> and <c>BusinessTiers</c>.
    /// </summary>
    public class AccountAdditionsQueries : IAccountAdditionsQueries
    {
        private readonly AccountContext _context;
        private readonly IMapper _mapper;

        public AccountAdditionsQueries(AccountContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Gets a specific <c>AnalysisProfile</c> by its id.
        /// </summary>
        /// <param name="id">The id of the <c>AnalysisProfile</c></param>
        /// <returns>A task that returns the <c>AnalysisProfile</c> when awaited.</returns>
        public async Task<AnalysisProfileViewModel> GetAnalysisProfileById(Guid id)
        {
            return await _context
                            .AnalysisProfiles
                            .AsNoTracking() // Do not track entities we have no intention of modifying.
                            .ProjectTo<AnalysisProfileViewModel>(_mapper.ConfigurationProvider)
                            .FirstOrDefaultAsync(ap => ap.Id == id);
        }

        /// <summary>
        /// Gets a list of all <c>AnalysisProfiles</c>.
        /// </summary>
        /// <returns>A task that returns list of <c>AnalysisProfiles</c> when awaited.</returns>
        public async Task<IEnumerable<AnalysisProfileViewModel>> GetAllAnalysisProfilesAsync()
        {
            return await _context
                            .AnalysisProfiles
                            .AsNoTracking()
                            .ProjectTo<AnalysisProfileViewModel>(_mapper.ConfigurationProvider)
                            .ToListAsync();
        }

        /// <summary>
        /// Gets a specific <c>BusinessTier</c> by its id.
        /// </summary>
        /// <param name="id">The id of the <c>BusinessTier</c></param>
        /// <returns>A task that returns the <c>BusinessTier</c> when awaited.</returns>
        public async Task<BusinessTierViewModel> GetBusinessTierById(Guid id)
        {
            return await _context
                            .BusinessTiers
                            .AsNoTracking()
                            .ProjectTo<BusinessTierViewModel>(_mapper.ConfigurationProvider)
                            .FirstOrDefaultAsync(bt => bt.Id == id);
        }

        /// <summary>
        /// Gets a list of all <c>BusinessTiers</c>.
        /// </summary>
        /// <returns>A task that returns list of <c>BusinessTiers</c> when awaited.</returns>
        public async Task<IEnumerable<BusinessTierViewModel>> GetAllBusinessTiersAsync()
        {
            return await _context
                            .BusinessTiers
                            .AsNoTracking()
                            .ProjectTo<BusinessTierViewModel>(_mapper.ConfigurationProvider)
                            .ToListAsync();
        }

        /// <summary>
        /// Gets a specific <c>Feature</c> by its id.
        /// </summary>
        /// <param name="id">The id of the <c>Feature</c></param>
        /// <returns>A task that returns the <c>Feature</c> when awaited.</returns>
        public async Task<FeatureViewModel> GetFeatureById(Guid id)
        {
            return await _context
                            .Features
                            .AsNoTracking()
                            .ProjectTo<FeatureViewModel>(_mapper.ConfigurationProvider)
                            .FirstOrDefaultAsync(f => f.Id == id);
        }

        /// <summary>
        /// Gets a list of all <c>Features</c>.
        /// </summary>
        /// <returns>A task that returns list of <c>Features</c> when awaited.</returns>
        public async Task<IEnumerable<FeatureViewModel>> GetAllFeaturesAsync()
        {
            return await _context
                            .Features
                            .AsNoTracking()
                            .ProjectTo<FeatureViewModel>(_mapper.ConfigurationProvider)
                            .ToListAsync();
        }
    }
}