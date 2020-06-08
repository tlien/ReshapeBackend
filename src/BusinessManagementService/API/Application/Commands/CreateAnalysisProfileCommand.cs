using System.Runtime.Serialization;
using MediatR;

namespace Reshape.BusinessManagementService.API.Application.Commands
{
    [DataContract]
    public class CreateAnalysisProfileCommand : IRequest<AnalysisProfileDTO>
    {
        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public string Description { get; private set; }

        [DataMember]
        public decimal Price { get; private set; }

        [DataMember]
        public MediaTypeDTO MediaType { get; private set; }

        [DataMember]
        public ScriptFileDTO ScriptFile { get; private set; }

        [DataMember]
        public ScriptParametersFileDTO ScriptParametersFile { get; private set; }

        public CreateAnalysisProfileCommand(string name, string description, decimal price, MediaTypeDTO mediaType, ScriptFileDTO scriptFile, ScriptParametersFileDTO scriptParametersFile)
        {
            Name = name;
            Description = description;
            Price = price;
            MediaType = mediaType;
            ScriptFile = scriptFile;
            ScriptParametersFile = scriptParametersFile;
        }
    }
}