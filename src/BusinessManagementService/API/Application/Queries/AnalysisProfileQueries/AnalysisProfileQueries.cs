using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using Reshape.BusinessManagementService.Infrastructure;

namespace Reshape.BusinessManagementService.API.Application.Queries.AnalysisProfileQueries
{
    public class AnalysisProfileQueries : IAnalysisProfileQueries
    {
        private readonly BusinessManagementContext _context;
        private readonly IMapper _mapper;

        public AnalysisProfileQueries(BusinessManagementContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<AnalysisProfileViewModel>> GetAllAsync()
        {
            return await _context.AnalysisProfiles
                .AsNoTracking()
                .ProjectTo<AnalysisProfileViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<AnalysisProfileViewModel> GetById(Guid id)
        {
            return await _context.AnalysisProfiles
                .AsNoTracking()
                .ProjectTo<AnalysisProfileViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}