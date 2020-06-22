using System;
using System.Runtime.Serialization;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Reshape.BusinessManagementService.API.Application.Commands
{
    /// <summary>
    /// Models the data needed to update a <c>ScriptFile</c> through the <c>UpdateScriptFileCommandHandler</c>.
    /// </summary>
    [DataContract]
    public class UpdateScriptFileCommand : IRequest<ScriptFileDTO>
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
        public string Script { get; private set; }

        public UpdateScriptFileCommand(Guid id, string name, string description, string script)
        {
            Id = id;
            Name = name;
            Description = description;
            Script = script;
        }
    }
}