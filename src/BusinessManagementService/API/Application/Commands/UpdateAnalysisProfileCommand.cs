using System;
using System.Runtime.Serialization;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Reshape.BusinessManagementService.API.Application.Commands
{
    [DataContract]
    public class UpdateAnalysisProfileCommand : IRequest<AnalysisProfileDTO>
    {
        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public Guid Id { get; set; }

        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public string Name { get; set; }

        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public string Description { get; set; }

        [DataMember]
        [SwaggerSchema(ReadOnly = false)]
        public decimal Price { get; set; }
    }
}