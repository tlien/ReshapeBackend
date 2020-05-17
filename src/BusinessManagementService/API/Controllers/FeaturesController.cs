using System;
using System.Threading.Tasks;
using BusinessManagementService.API.Application.Commands;
using BusinessManagementService.API.Application.Queries.FeatureQueries;
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
        private readonly IFeatureQueries _featureQueries;

        public FeaturesController(IMediator mediator, IFeatureQueries featureQueries)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _featureQueries = featureQueries ?? throw new ArgumentNullException(nameof(featureQueries));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _featureQueries.GetAllAsync());
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            return Ok(await _featureQueries.GetById(id));
        }

        [HttpPost]
        public async Task<ActionResult<FeatureDTO>> AddAsync([FromBody] CreateFeatureCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}