using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;

using Reshape.BusinessManagementService.API.Application.Queries.FeatureQueries;
using Reshape.BusinessManagementService.API.Application.Commands;

namespace Reshape.BusinessManagementService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
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
            var feature = await _featureQueries.GetById(id);

            if (feature == null)
            {
                return NotFound();
            }

            return Ok(feature);
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] CreateFeatureCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateFeatureCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}