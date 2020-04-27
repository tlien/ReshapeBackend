using System.Threading.Tasks;
using Common.SeedWork;

namespace BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate {
    public interface IBusinessTierRepository : IRepository<BusinessTier> {
        BusinessTier Add(BusinessTier businessTier);
        void Update(BusinessTier businessTier);
        void Remove(string id);
        Task<BusinessTier> GetAsync(string id);
    }
}