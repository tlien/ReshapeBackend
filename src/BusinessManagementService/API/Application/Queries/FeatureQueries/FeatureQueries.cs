using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using Reshape.BusinessManagementService.Infrastructure;

namespace Reshape.BusinessManagementService.API.Application.Queries.FeatureQueries
{
    /// <summary>
    /// Holds database queries to get <c>Features</c>
    /// </summary>
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
                .AsNoTracking()
                .ProjectTo<FeatureViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<FeatureViewModel> GetById(Guid id)
        {
            return await _context.Features
                .AsNoTracking()
                .ProjectTo<FeatureViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}