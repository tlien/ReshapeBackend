using System;
using System.Threading.Tasks;

using Reshape.Common.SeedWork;

namespace Reshape.BusinessManagementService.Domain.AggregatesModel.FeatureAggregate
{
    public interface IFeatureRepository : IRepository<Feature>
    {
        Feature Add(Feature feature);
        Task<Feature> GetAsync(Guid id);
        void Update(Feature feature);
    }
}