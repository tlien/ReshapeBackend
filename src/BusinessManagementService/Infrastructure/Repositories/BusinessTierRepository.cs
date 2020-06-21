using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Reshape.Common.SeedWork;
using Reshape.BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate;

namespace Reshape.BusinessManagementService.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for the <c>BusinessTier</c> domain aggregate.
    /// Implements <c>IBusinessTierRepository</c>
    /// </summary>
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

        /// <summary>
        /// Adds a <c>BusinessTier</c> to the <c>BusinessManagementContext</c> in-memory
        /// </summary>
        /// <param name="businessTier"></param>
        /// <returns>The added <c>BusinessTier</c> entity</returns>
        public BusinessTier Add(BusinessTier businessTier)
        {
            return _context.BusinessTiers.Add(businessTier).Entity;
        }

        /// <summary>
        /// Gets a single <c>BusinessTier</c> by id.
        /// If the <c>BusinessTier</c> does not exist in the database,
        /// an additional in-memory look-up will take place.
        /// </summary>
        /// <param name="id">The <c>BusinessTier.Id</c></param>
        /// <returns>A Task that returns the retrieved <c>BusinessTier</c> when awaited.</returns>
        public async Task<BusinessTier> GetAsync(Guid id)
        {
            var businessTier = await _context.BusinessTiers.FirstOrDefaultAsync(b => b.Id == id);

            if (businessTier == null)
            {
                businessTier = _context.BusinessTiers.Local.FirstOrDefault(b => b.Id == id);
            }

            return businessTier;
        }

        /// <summary>
        /// Sets the change tracking <c>State</c>
        /// of a <c>BusinessTier</c> to <c>EntityState.Modified</c>.
        /// This allows changes that have been made to the tracked <c>BusinessTier</c> to be
        /// committed when changes are saved to the database context.
        /// </summary>
        /// <param name="businessTier"></param>
        public void Update(BusinessTier businessTier)
        {
            _context.Entry(businessTier).State = EntityState.Modified;
        }
    }
}