using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using Reshape.BusinessManagementService.Infrastructure;

namespace Reshape.BusinessManagementService.API.Application.Queries.BusinessTierQueries
{
    /// <summary>
    /// Holds database queries to get <c>BusinessTiers</c>
    /// </summary>
    public class BusinessTierQueries : IBusinessTierQueries
    {
        private readonly BusinessManagementContext _context;
        private readonly IMapper _mapper;

        public BusinessTierQueries(BusinessManagementContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<BusinessTierViewModel>> GetAllAsync()
        {
            return await _context.BusinessTiers
                .AsNoTracking()
                .ProjectTo<BusinessTierViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<BusinessTierViewModel> GetById(Guid id)
        {
            return await _context.BusinessTiers
                .AsNoTracking()
                .ProjectTo<BusinessTierViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}