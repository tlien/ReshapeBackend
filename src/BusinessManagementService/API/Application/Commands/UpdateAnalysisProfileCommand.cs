using System;
using System.Runtime.Serialization;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Reshape.BusinessManagementService.API.Application.Commands
{
    [DataContract]
    public class UpdateAnalysisProfileCommand : IRequest<AnalysisProfileDTO>
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
        public decimal Price { get; private set; }

        public UpdateAnalysisProfileCommand(Guid id, string name, string description, decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
        }
    }
}