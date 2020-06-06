using System.Runtime.Serialization;
using MediatR;
using static Reshape.BusinessManagementService.API.Application.Commands.CreateFeatureCommandHandler;

namespace Reshape.BusinessManagementService.API.Application.Commands
{   
    [DataContract]
    public class CreateFeatureCommand : IRequest<FeatureDTO>
    {
        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public string Description { get; private set; }

        [DataMember]
        public decimal Price { get; set; }

        public CreateFeatureCommand(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }
    }
}