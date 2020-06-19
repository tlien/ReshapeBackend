using System;
using System.Threading.Tasks;

using Reshape.Common.SeedWork;

namespace Reshape.BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate
{
    public interface IBusinessTierRepository : IRepository<BusinessTier>
    {
        BusinessTier Add(BusinessTier businessTier);
        Task<BusinessTier> GetAsync(Guid id);
        void Update(BusinessTier businessTier);
    }
}