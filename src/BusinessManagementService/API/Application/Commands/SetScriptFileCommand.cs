using System;
using System.Runtime.Serialization;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Reshape.BusinessManagementService.API.Application.Commands
{
    [DataContract]
    public class SetScriptFileCommand : IRequest<AnalysisProfileDTO>
    {
        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public Guid AnalysisProfileId { get; private set; }

        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public Guid ScriptFileId { get; private set; }

        public SetScriptFileCommand(Guid analysisProfileId, Guid scriptFileId)
        {
            AnalysisProfileId = analysisProfileId;
            ScriptFileId = scriptFileId;
        }
    }
}