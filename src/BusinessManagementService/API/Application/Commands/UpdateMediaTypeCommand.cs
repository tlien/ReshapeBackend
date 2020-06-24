using System;
using System.Runtime.Serialization;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Reshape.BusinessManagementService.API.Application.Commands
{
    /// <summary>
    /// Models the data needed to update a <c>MediaType</c> through the <c>UpdateMediaTypeCommandHandler</c>.
    /// </summary>
    [DataContract]
    public class UpdateMediaTypeCommand : IRequest<MediaTypeDTO>
    {
        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public Guid Id { get; private set; }

        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public string Name { get; private set; }

        public UpdateMediaTypeCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}