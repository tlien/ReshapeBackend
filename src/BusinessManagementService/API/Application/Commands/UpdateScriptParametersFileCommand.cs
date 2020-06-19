using System;
using System.Runtime.Serialization;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Reshape.BusinessManagementService.API.Application.Commands
{
    [DataContract]
    public class UpdateScriptParametersFileCommand : IRequest<ScriptParametersFileDTO>
    {
        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public Guid Id { get; set; }

        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public string Name { get; set; }

        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public string Description { get; set; }

        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public string ScriptParameters { get; set; }
    }
}