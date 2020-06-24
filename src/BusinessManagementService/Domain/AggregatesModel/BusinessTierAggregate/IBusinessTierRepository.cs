using System;
using System.Threading.Tasks;

using Reshape.Common.SeedWork;

namespace Reshape.BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate
{
    /// <summary>
    /// Repository interface stating the methods necessary
    /// to handle database writes to the <c>BusinessTier</c> domain aggregate.
    /// </summary>
    public interface IBusinessTierRepository : IRepository<BusinessTier>
    {
        BusinessTier Add(BusinessTier businessTier);
        Task<BusinessTier> GetAsync(Guid id);
        void Update(BusinessTier businessTier);
    }
}