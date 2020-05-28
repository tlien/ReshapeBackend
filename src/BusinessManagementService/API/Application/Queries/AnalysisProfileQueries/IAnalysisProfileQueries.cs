using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reshape.BusinessManagementService.API.Application.Queries.AnalysisProfileQueries
{
    public interface IAnalysisProfileQueries
    {
        Task<IEnumerable<AnalysisProfileViewModel>> GetAllAsync();
        Task<AnalysisProfileViewModel> GetById(Guid id);
    }
}