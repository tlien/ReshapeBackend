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
        public Guid AnalysisProfileId { get; set; }

        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public Guid ScriptParametersFileId { get; set; }
    }
}