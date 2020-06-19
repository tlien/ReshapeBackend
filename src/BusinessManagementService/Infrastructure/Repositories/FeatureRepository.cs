using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Reshape.Common.SeedWork;
using Reshape.BusinessManagementService.Domain.AggregatesModel.FeatureAggregate;

namespace Reshape.BusinessManagementService.Infrastructure.Repositories
{
    public class FeatureRepository : IFeatureRepository
    {
        private readonly BusinessManagementContext _context;
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

        public Feature Add(Feature feature)
        {
            return _context.Features.Add(feature).Entity;
        }

        public async Task<Feature> GetAsync(Guid id)
        {
            var feature = await _context.Features.FirstOrDefaultAsync(f => f.Id == id);

            if (feature == null)
            {
                feature = _context.Features.Local.FirstOrDefault(f => f.Id == id);
            }

            return feature;
        }

        public void Update(Feature feature)
        {
            _context.Entry(feature).State = EntityState.Modified;
        }
    }
}