using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Reshape.Common.SeedWork;
using Reshape.BusinessManagementService.Domain.AggregatesModel.FeatureAggregate;

namespace Reshape.BusinessManagementService.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for the <c>Feature</c> domain aggregate.
    /// Implements <c>IFeatureRepository</c>
    /// </summary>
    public class FeatureRepository : IFeatureRepository
    {
        private readonly BusinessManagementContext _context;

        /// <summary>
        /// Gets underlying DbContext (DbContexts implement the Unit of Work pattern).
        /// </summary>
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public FeatureRepository(BusinessManagementContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Adds a <c>Feature</c> to the <c>BusinessManagementContext</c> in-memory
        /// </summary>
        /// <param name="feature"></param>
        /// <returns>The added <c>Feature</c> entity</returns>
        public Feature Add(Feature feature)
        {
            return _context.Features.Add(feature).Entity;
        }

        /// <summary>
        /// Gets a single <c>Feature</c> by id.
        /// If the <c>Feature</c> does not exist in the database,
        /// an additional in-memory look-up will take place.
        /// </summary>
        /// <param name="id">The <c>Feature.Id</c></param>
        /// <returns>A Task that returns the retrieved <c>Feature</c> when awaited.</returns>
        public async Task<Feature> GetAsync(Guid id)
        {
            var feature = await _context.Features.FirstOrDefaultAsync(f => f.Id == id);

            if (feature == null)
            {
                feature = _context.Features.Local.FirstOrDefault(f => f.Id == id);
            }

            return feature;
        }

        /// <summary>
        /// Sets the change tracking <c>State</c>
        /// of a <c>Feature</c> to <c>EntityState.Modified</c>.
        /// This allows changes that have been made to the tracked <c>Feature</c> to be
        /// committed when changes are saved to the database context.
        /// </summary>
        /// <param name="feature"></param>
        public void Update(Feature feature)
        {
            _context.Entry(feature).State = EntityState.Modified;
        }
    }
}