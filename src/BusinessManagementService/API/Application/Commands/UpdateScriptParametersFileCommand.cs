using System;
using System.Runtime.Serialization;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Reshape.BusinessManagementService.API.Application.Commands
{
    /// <summary>
    /// Models the data needed to update a <c>ScriptParametersFile</c> through the <c>UpdateScriptParametersFileCommandHandler</c>.
    /// </summary>
    [DataContract]
    public class UpdateScriptParametersFileCommand : IRequest<ScriptParametersFileDTO>
    {
        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public Guid Id { get; private set; }

        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public string Name { get; private set; }

        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public string Description { get; private set; }

        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public string ScriptParameters { get; private set; }

        public UpdateScriptParametersFileCommand(Guid id, string name, string description, string scriptParameters)
        {
            Id = id;
            Name = name;
            Description = description;
            ScriptParameters = scriptParameters;
        }
    }
}