using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reshape.BusinessManagementService.Infrastructure;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Reshape.BusinessManagementService.API.Application.Queries.BusinessTierQueries
{
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
                .ProjectTo<BusinessTierViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<BusinessTierViewModel> GetById(Guid id)
        {
            return await _context.BusinessTiers
                .ProjectTo<BusinessTierViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}