using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;

using Reshape.BusinessManagementService.API.Application.Commands;
using Reshape.BusinessManagementService.API.Application.Queries.BusinessTierQueries;

namespace Reshape.BusinessManagementService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class BusinessTiersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IBusinessTierQueries _businessTierQueries;

        public BusinessTiersController(IMediator mediator, IBusinessTierQueries businessTierQueries)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _businessTierQueries = businessTierQueries ?? throw new ArgumentNullException(nameof(businessTierQueries));
        }

        /// <summary>
        /// Gets all <c>BusinessTiers</c>
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _businessTierQueries.GetAllAsync());
        }

        /// <summary>
        /// Gets a single <c>BusinessTier</c> by its UUID
        /// </summary>
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var businessTier = await _businessTierQueries.GetById(id);

            if (businessTier == null)
            {
                return NotFound();
            }

            return Ok(businessTier);
        }

        /// <summary>
        /// Created a new <c>BusinessTier</c>
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateBusinessTierCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Updates the full content of a <c>BusinessTier</c>
        /// </summary>
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateBusinessTierCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}