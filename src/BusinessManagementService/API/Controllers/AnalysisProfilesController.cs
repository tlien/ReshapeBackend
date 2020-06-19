using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;

using Reshape.BusinessManagementService.API.Application.Commands;
using Reshape.BusinessManagementService.API.Application.Queries.AnalysisProfileQueries;

namespace Reshape.BusinessManagementService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class AnalysisProfilesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAnalysisProfileQueries _analysisProfileQueries;

        public AnalysisProfilesController(IMediator mediator, IAnalysisProfileQueries analysisProfileQueries)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _analysisProfileQueries = analysisProfileQueries ?? throw new ArgumentNullException(nameof(analysisProfileQueries));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _analysisProfileQueries.GetAllAsync());
        }

        [Route("{id:Guid}")]
        [HttpGet]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var analysisProfile = await _analysisProfileQueries.GetById(id);

            if (analysisProfile == null)
            {
                return NotFound();
            }

            return Ok(analysisProfile);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateAnalysisProfileCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Route("mediatype")]
        [HttpPut]
        public async Task<IActionResult> SetMediaType([FromBody] SetMediaTypeCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // [Route("update")]
        // [HttpPut]
        // public Task<IActionResult> UpdateAsync()
        // {
        // }

        // [Route("delete")]
        // [HttpDelete]
        // public Task<IActionResult> DeleteAsync()
        // {
        // }
    }
}