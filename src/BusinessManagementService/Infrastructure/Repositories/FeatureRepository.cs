using System;
using System.Linq;
using System.Threading.Tasks;
using BusinessManagementService.Domain.AggregatesModel.FeatureAggregate;
using Common.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementService.Infrastructure.Repositories 
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

        public async void Remove(Guid id)
        {
            var feature = await _context.Features.FirstOrDefaultAsync(f => f.Id == id);
            // _context.Features.Remove(feature);
            _context.Entry(feature).State = EntityState.Deleted;

            // @TODO: Launch domain event from command handler? Features are not likely to be removed.
        }

        public void Update(Feature feature)
        {
            _context.Entry(feature).State = EntityState.Modified;
        }
    }
}