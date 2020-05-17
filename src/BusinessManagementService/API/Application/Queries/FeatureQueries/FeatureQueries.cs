using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessManagementService.Infrastructure;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementService.API.Application.Queries.FeatureQueries
{
    public class FeatureQueries : IFeatureQueries
    {
        private readonly BusinessManagementContext _context;
        private readonly IMapper _mapper;

        public FeatureQueries(BusinessManagementContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<FeatureViewModel>> GetAllAsync()
        {
            return await _context.Features
                .ProjectTo<FeatureViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<FeatureViewModel> GetById(Guid id)
        {
            return await _context.Features
                .ProjectTo<FeatureViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}