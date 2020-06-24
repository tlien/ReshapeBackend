using System;
using System.Runtime.Serialization;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Reshape.BusinessManagementService.API.Application.Commands
{
    /// <summary>
    /// Models the data needed to set the <c>MediaType</c> relation of an <c>AnalysisProfile</c> through the <c>SetMediaTypeCommandHandler</c>
    /// </summary>
    [DataContract]
    public class SetMediaTypeCommand : IRequest<AnalysisProfileDTO>
    {
        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public Guid AnalysisProfileId { get; private set; }

        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public Guid MediaTypeId { get; private set; }

        public SetMediaTypeCommand(Guid analysisProfileId, Guid mediaTypeId)
        {
            AnalysisProfileId = analysisProfileId;
            MediaTypeId = mediaTypeId;
        }
    }
}