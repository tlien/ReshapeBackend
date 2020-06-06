using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reshape.BusinessManagementService.API.Application.Queries.BusinessTierQueries
{
    public interface IBusinessTierQueries
    {
        Task<IEnumerable<BusinessTierViewModel>> GetAllAsync();
        Task<BusinessTierViewModel> GetById(Guid id);
    }
}