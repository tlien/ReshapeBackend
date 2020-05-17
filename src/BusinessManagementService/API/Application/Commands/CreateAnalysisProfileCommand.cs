using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MediatR;
using static BusinessManagementService.API.Application.Commands.CreateAnalysisProfileCommandHandler;

namespace BusinessManagementService.API.Application.Commands
{
    [DataContract]
    public class CreateAnalysisProfileCommand : IRequest<AnalysisProfileDTO>
    {
        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public string Description { get; private set; }

        [DataMember]
        public string FileName { get; private set; }

        [DataMember]
        private readonly List<AnalysisProfileRequiredFeatureDTO> _requiredFeatures;

        [DataMember]
        public IEnumerable<AnalysisProfileRequiredFeatureDTO> RequiredFeatures => _requiredFeatures;

        public CreateAnalysisProfileCommand()
        {
            _requiredFeatures = new List<AnalysisProfileRequiredFeatureDTO>();
        }

        public CreateAnalysisProfileCommand(string name, string description, string fileName,
            List<AnalysisProfileRequiredFeatureDTO> requiredFeatures) : this()
        {
            Name = name;
            Description = description;
            FileName = fileName;
            _requiredFeatures = requiredFeatures;
        }

        public class AnalysisProfileRequiredFeatureDTO
        {
            public Guid AnalysisProfileID { get; set; }
            public Guid FeatureID { get; set; }
        }
    }
}