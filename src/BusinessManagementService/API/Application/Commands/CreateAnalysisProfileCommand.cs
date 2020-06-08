using System.Runtime.Serialization;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Reshape.BusinessManagementService.API.Application.Commands
{
    [DataContract]
    public class CreateAnalysisProfileCommand : IRequest<AnalysisProfileDTO>
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

        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public MediaTypeDTO MediaType { get; private set; }

        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public ScriptFileDTO ScriptFile { get; private set; }

        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
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