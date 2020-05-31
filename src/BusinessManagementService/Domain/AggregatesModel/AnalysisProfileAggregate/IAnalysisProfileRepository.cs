
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reshape.Common.SeedWork;

namespace Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate {
    public interface IAnalysisProfileRepository : IRepository<AnalysisProfile> {
        AnalysisProfile Add(AnalysisProfile analysisProfile);
        void Update(AnalysisProfile analysisProfile);
        void Remove(Guid id);
        Task<AnalysisProfile> GetAsync(Guid id);
        Task<List<AnalysisProfile>> GetAllAsync();
        MediaType AddMediaType(MediaType mediaType);
        ScriptFile AddScriptFile(ScriptFile scriptFile);
        ScriptParametersFile AddScriptParametersFile(ScriptParametersFile scriptParametersFile);
    }
}