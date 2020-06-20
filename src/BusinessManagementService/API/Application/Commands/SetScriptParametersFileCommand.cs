using System;
using System.Runtime.Serialization;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Reshape.BusinessManagementService.API.Application.Commands
{
    [DataContract]
    public class SetScriptParametersFileCommand : IRequest<AnalysisProfileDTO>
    {
        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public Guid AnalysisProfileId { get; private set; }

        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public Guid ScriptParametersFileId { get; private set; }

        public SetScriptParametersFileCommand(Guid analysisProfileId, Guid scriptParametersFileId)
        {
            AnalysisProfileId = analysisProfileId;
            ScriptParametersFileId = scriptParametersFileId;
        }
    }
}