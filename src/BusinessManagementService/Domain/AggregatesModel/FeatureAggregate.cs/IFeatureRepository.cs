using System.Threading.Tasks;
using Common.SeedWork;

namespace BusinessManagementService.Domain.AggregatesModel.FeatureAggregate {
    public interface IFeatureRepository : IRepository<Feature> {
        Feature Add(Feature feature);
        void Update(Feature feature);
        void Remove(string id);
        Task<Feature> GetAsync(string id);
    }
}