using System.Runtime.Serialization;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Reshape.BusinessManagementService.API.Application.Commands
{
    /// <summary>
    /// Models the data needed to create a new <c>Feature</c> through the <c>CreateFeatureCommandHandler</c>
    /// </summary>
    [DataContract]
    public class CreateFeatureCommand : IRequest<FeatureDTO>
    {
        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public string Name { get; private set; }

        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public string Description { get; private set; }

        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public decimal Price { get; private set; }

        public CreateFeatureCommand(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }
    }
}