using System;
using MediatR;

namespace Reshape.BusinessManagementService.API.Application.Commands
{
    public class SetMediaTypeCommand : IRequest<AnalysisProfileDTO>
    {
        public Guid AnalysisProfileId { get; set; }
        public Guid MediaTypeId { get; set; }
    }
}