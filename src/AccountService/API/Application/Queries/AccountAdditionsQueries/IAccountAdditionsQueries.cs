using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reshape.AccountService.API.Application.Queries.AccountAdditionsQueries
{
    public interface IAccountAdditionsQueries
    {
        Task<IEnumerable<FeatureViewModel>> GetAllFeaturesAsync();
        Task<FeatureViewModel> GetFeatureById(Guid id);

        Task<IEnumerable<BusinessTierViewModel>> GetAllBusinessTiersAsync();
        Task<BusinessTierViewModel> GetBusinessTierById(Guid id);

        Task<IEnumerable<AnalysisProfileViewModel>> GetAllAnalysisProfilesAsync();
        Task<AnalysisProfileViewModel> GetAnalysisProfileById(Guid id);
    }
}