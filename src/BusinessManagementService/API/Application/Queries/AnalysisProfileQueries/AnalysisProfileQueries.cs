using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using Npgsql;
using System.Data;
using BusinessManagementService.Infrastructure;
using System.Linq;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementService.API.Application.Queries.AnalysisProfileQueries
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
                .ProjectTo<AnalysisProfileViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<AnalysisProfileViewModel> GetById(Guid id)
        {
            return await _context.AnalysisProfiles
                .ProjectTo<AnalysisProfileViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}