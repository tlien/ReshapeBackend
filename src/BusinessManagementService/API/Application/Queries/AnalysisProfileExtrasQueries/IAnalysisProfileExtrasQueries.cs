using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Reshape.BusinessManagementService.API.Application.Queries.AnalysisProfileQueries;

namespace Reshape.BusinessManagementService.API.Application.Queries.AnalysisProfileExtrasQueries
{
    public interface IAnalysisProfileExtrasQueries
    {
        Task<IEnumerable<ScriptFileViewModel>> GetScriptFiles();
        Task<IEnumerable<ScriptParametersFileViewModel>> GetScriptParametersFiles();
        Task<IEnumerable<MediaTypeViewModel>> GetMediaTypes();
        Task<ScriptFileViewModel> GetScriptFileById(Guid id);
        Task<ScriptParametersFileViewModel> GetScriptParametersFileById(Guid id);
        Task<MediaTypeViewModel> GetMediaTypeById(Guid id);
    }
}