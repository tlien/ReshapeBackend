using System.Runtime.Serialization;
using MediatR;

namespace BusinessManagementService.API.Application.Commands
{
    [DataContract]
    public class CreateBusinessTierCommand : IRequest<BusinessTierDTO>
    {
        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public string Description { get; private set; }

        [DataMember]
        public decimal Price { get; private set; }


        public CreateBusinessTierCommand(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }
    }


}