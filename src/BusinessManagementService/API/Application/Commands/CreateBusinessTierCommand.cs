using System.Runtime.Serialization;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Reshape.BusinessManagementService.API.Application.Commands
{
    /// <summary>
    /// Models the data needed to create a new <c>BusinessTier</c> through the <c>CreateBusinessTierCommandHandler</c>
    /// </summary>
    [DataContract]
    public class CreateBusinessTierCommand : IRequest<BusinessTierDTO>
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


        public CreateBusinessTierCommand(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }
    }


}