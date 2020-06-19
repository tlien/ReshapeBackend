using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reshape.BusinessManagementService.API.Application.Queries.FeatureQueries
{
    public interface IFeatureQueries
    {
        Task<IEnumerable<FeatureViewModel>> GetAllAsync();
        Task<FeatureViewModel> GetById(Guid id);
    }
}