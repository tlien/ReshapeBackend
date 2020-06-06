using System;
using System.Linq;
using System.Threading.Tasks;
using Reshape.BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate;
using Reshape.Common.SeedWork;
using Microsoft.EntityFrameworkCore;

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

        public async void Remove(Guid id)
        {
            var businessTier = await _context.BusinessTiers.FirstOrDefaultAsync(b => b.Id == id);
            // _context.BusinessTiers.Remove(businessTier);
            _context.Entry(businessTier).State = EntityState.Deleted;
        }

        public void Update(BusinessTier businessTier)
        {
            throw new System.NotImplementedException();
        }
    }
}