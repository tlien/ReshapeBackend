
using System.Threading.Tasks;
using Common.SeedWork;

namespace BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate {
    public interface IAnalysisProfileRepository : IRepository<AnalysisProfile> {
        AnalysisProfile Add(AnalysisProfile analysisProfile);
        void Update(AnalysisProfile analysisProfile);
        void Remove(string id);
        Task<AnalysisProfile> GetAsync(string id);
    }
}