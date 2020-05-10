using System;
using System.Threading.Tasks;
using BusinessManagementService.API.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static BusinessManagementService.API.Application.Commands.CreateFeatureCommandHandler;

namespace BusinessManagementService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IMediator _mediator;
        // private readonly IFeatureQueries _featureQueries;

        public FeaturesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<ActionResult<FeatureDTO>> AddAsync([FromBody] CreateFeatureCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}