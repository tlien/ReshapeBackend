using System;
using System.Threading.Tasks;
using Common.SeedWork;

namespace BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate {
    public interface IBusinessTierRepository : IRepository<BusinessTier> 
    {
        BusinessTier Add(BusinessTier businessTier);
        void Update(BusinessTier businessTier);
        void Remove(Guid id);
        Task<BusinessTier> GetAsync(Guid id);
    }
}