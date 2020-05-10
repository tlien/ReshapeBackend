using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessManagementService.API.Application.Queries.AnalysisProfileQueries
{
    public interface IAnalysisProfileQueries
    {
        Task<IEnumerable<AnalysisProfileViewModel>> GetAllAsync();
        Task<AnalysisProfileViewModel> GetById(Guid id);
    }
}