using System;
using System.Threading.Tasks;
using Reshape.Common.SeedWork;

namespace Reshape.BusinessManagementService.Domain.AggregatesModel.FeatureAggregate {
    public interface IFeatureRepository : IRepository<Feature> 
    {
        Feature Add(Feature feature);
        void Update(Feature feature);
        void Remove(Guid id);
        Task<Feature> GetAsync(Guid id);
    }
}