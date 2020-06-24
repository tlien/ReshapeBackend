using System;
using System.Runtime.Serialization;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Reshape.BusinessManagementService.API.Application.Commands
{
    /// <summary>
    /// Models the data needed to update a <c>Feature</c> through the <c>UpdateFeatureCommandHandler</c>
    /// </summary>
    [DataContract]
    public class UpdateFeatureCommand : IRequest<FeatureDTO>
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

        public UpdateFeatureCommand(Guid id, string name, string description, decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
        }
    }
}