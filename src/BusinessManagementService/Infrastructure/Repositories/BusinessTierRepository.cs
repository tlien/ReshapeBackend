using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Reshape.Common.SeedWork;
using Reshape.BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate;

namespace Reshape.BusinessManagementService.Infrastructure.Repositories
{
    public class BusinessTierRepository : IBusinessTierRepository
    {
        private readonly BusinessManagementContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public BusinessTierRepository(BusinessManagementContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public BusinessTier Add(BusinessTier businessTier)
        {
            return _context.BusinessTiers.Add(businessTier).Entity;
        }

        public async Task<BusinessTier> GetAsync(Guid id)
        {
            var businessTier = await _context.BusinessTiers.FirstOrDefaultAsync(b => b.Id == id);

            if (businessTier == null)
            {
                businessTier = _context.BusinessTiers.Local.FirstOrDefault(b => b.Id == id);
            }

            return businessTier;
        }

        public void Update(BusinessTier businessTier)
        {
            _context.Entry(businessTier).State = EntityState.Modified;
        }
    }
}