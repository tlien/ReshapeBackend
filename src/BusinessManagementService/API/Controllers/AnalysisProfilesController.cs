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

        /// <summary>
        /// Gets all AnalysisProfiles
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _analysisProfileQueries.GetAllAsync());
        }

        /// <summary>
        /// Gets a single AnalysisProfile by its UUID
        /// </summary>
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

        /// <summary>
        /// Creates a new AnalysisProfile
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateAnalysisProfileCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Updates basic information of an AnalysisProfile, not related entities
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateAnalysisProfileCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Sets the MediaType relation of an AnalysisProfile
        /// </summary>
        [Route("mediatype")]
        [HttpPut]
        public async Task<IActionResult> SetMediaType([FromBody] SetMediaTypeCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Sets the ScriptFile relation of an AnalysisProfile
        /// </summary>
        [Route("scriptfile")]
        [HttpPut]
        public async Task<IActionResult> SetScriptFile([FromBody] SetScriptFileCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Sets the ScriptParametersFile relation of an AnalysisProfile
        /// </summary>
        [Route("scriptparametersfile")]
        [HttpPut]
        public async Task<IActionResult> SetScriptParametersFile([FromBody] SetScriptParametersFileCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}