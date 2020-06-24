
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Reshape.Common.SeedWork;

namespace Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate
{
    /// <summary>
    /// Repository interface stating the methods necessary
    /// to handle database writes to the <c>AnalysisProfile</c>
    /// domain aggregate and its related entities.
    /// </summary>
    public interface IAnalysisProfileRepository : IRepository<AnalysisProfile>
    {
        Task<List<AnalysisProfile>> GetAllAsync();
        Task<AnalysisProfile> GetAsync(Guid id);
        Task<MediaType> GetMediaTypeAsync(Guid id);
        Task<ScriptFile> GetScriptFileAsync(Guid sid);
        Task<ScriptParametersFile> GetScriptParametersFileAsync(Guid id);
        AnalysisProfile Add(AnalysisProfile analysisProfile);
        MediaType AddMediaType(MediaType mediaType);
        ScriptFile AddScriptFile(ScriptFile scriptFile);
        ScriptParametersFile AddScriptParametersFile(ScriptParametersFile scriptParametersFile);
        void Update(AnalysisProfile analysisProfile);
        void UpdateMediaType(MediaType mediaType);
        void UpdateScriptFile(ScriptFile scriptFile);
        void UpdateScriptParametersFile(ScriptParametersFile scriptParametersFile);
    }
}