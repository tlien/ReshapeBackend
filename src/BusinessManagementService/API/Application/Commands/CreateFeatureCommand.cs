using System;
using System.Runtime.Serialization;
using MediatR;
using static BusinessManagementService.API.Application.Commands.CreateFeatureCommandHandler;

namespace BusinessManagementService.API.Application.Commands
{   
    [DataContract]
    public class CreateFeatureCommand : IRequest<FeatureDTO>
    {
        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public string Description { get; private set; }

        public CreateFeatureCommand(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}