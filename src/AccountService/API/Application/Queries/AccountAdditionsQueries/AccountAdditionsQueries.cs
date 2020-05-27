using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper;

using Reshape.AccountService.Infrastructure;

namespace Reshape.AccountService.API.Application.Queries.AccountAdditionsQueries
{
    public class AccountAdditionsQueries : IAccountAdditionsQueries
    {
        private readonly AccountContext _context;
        private readonly IMapper _mapper;

        public AccountAdditionsQueries(AccountContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<AnalysisProfileViewModel> GetAnalysisProfileById(Guid id)
        {
            return await _context
                            .AnalysisProfiles
                            .ProjectTo<AnalysisProfileViewModel>(_mapper.ConfigurationProvider)
                            .FirstOrDefaultAsync(ap => ap.Id == id);
        }

        public async Task<IEnumerable<AnalysisProfileViewModel>> GetAllAnalysisProfilesAsync()
        {
            return await _context
                            .AnalysisProfiles
                            .ProjectTo<AnalysisProfileViewModel>(_mapper.ConfigurationProvider)
                            .ToListAsync();
        }

        public async Task<BusinessTierViewModel> GetBusinessTierById(Guid id)
        {
            return await _context
                            .BusinessTiers
                            .ProjectTo<BusinessTierViewModel>(_mapper.ConfigurationProvider)
                            .FirstOrDefaultAsync(bt => bt.Id == id);
        }

        public async Task<IEnumerable<BusinessTierViewModel>> GetAllBusinessTiersAsync()
        {
            return await _context
                            .BusinessTiers
                            .ProjectTo<BusinessTierViewModel>(_mapper.ConfigurationProvider)
                            .ToListAsync();
        }

        public async Task<FeatureViewModel> GetFeatureById(Guid id)
        {
            return await _context
                            .Features
                            .ProjectTo<FeatureViewModel>(_mapper.ConfigurationProvider)
                            .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<FeatureViewModel>> GetAllFeaturesAsync()
        {
            return await _context
                            .Features
                            .ProjectTo<FeatureViewModel>(_mapper.ConfigurationProvider)
                            .ToListAsync();
        }
    }
}