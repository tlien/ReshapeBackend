using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;

using Reshape.BusinessManagementService.API.Application.Queries.FeatureQueries;
using Reshape.BusinessManagementService.API.Application.Commands;
using static Reshape.BusinessManagementService.API.Application.Commands.CreateFeatureCommandHandler;
using Microsoft.AspNetCore.Authorization;

namespace Reshape.BusinessManagementService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
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
            return Ok(await _mediator.Send(command));
        }
    }
}