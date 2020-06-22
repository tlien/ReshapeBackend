using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reshape.BusinessManagementService.API.Application.Commands;
using Reshape.BusinessManagementService.API.Application.Queries.AnalysisProfileExtrasQueries;

namespace Reshape.BusinessManagementService.API.Controllers
{

    /// <summary>
    /// Access resources that are otherwise directly tied to the AnalysisProfile aggregate.
    /// This controller, and all commands and queries fired inside it, inherently go against the DDD principle that
    /// entity data should be read and written through a parent domain aggregate.
    ///
    /// It is present to accommodate CRUD features that the nature of the BusinessManagementService immanently calls for.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class AnalysisProfileExtrasController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IAnalysisProfileExtrasQueries _analysisProfileExtrasQueries;

        public AnalysisProfileExtrasController(IMediator mediator, IAnalysisProfileExtrasQueries analysisProfileExtrasQueries)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _analysisProfileExtrasQueries = analysisProfileExtrasQueries ?? throw new ArgumentNullException(nameof(analysisProfileExtrasQueries));
        }

        /// <summary>
        /// Gets all <c>ScriptFiles</c>
        /// </summary>
        [HttpGet]
        [Route("scriptfiles")]
        public async Task<ActionResult<IEnumerable<ScriptFileDTO>>> GetScriptFiles()
        {
            return Ok(await _analysisProfileExtrasQueries.GetScriptFiles());
        }

        /// <summary>
        /// Gets all <c>ScriptParametersFiles</c>
        /// </summary>
        [HttpGet]
        [Route("scriptparametersfiles")]
        public async Task<IActionResult> GetScriptParametersFiles()
        {
            return Ok(await _analysisProfileExtrasQueries.GetScriptParametersFiles());
        }

        /// <summary>
        /// Gets all <c>MediaTypes</c>
        /// </summary>
        [HttpGet]
        [Route("mediatypes")]
        public async Task<IActionResult> GetMediaTypes()
        {
            return Ok(await _analysisProfileExtrasQueries.GetMediaTypes());
        }

        /// <summary>
        /// Gets a single <c>ScriptFile</c> by its UUID
        /// </summary>
        [HttpGet]
        [Route("scriptfiles/{id:Guid}")]
        public async Task<IActionResult> GetScriptFileById(Guid id)
        {
            var scriptFile = await _analysisProfileExtrasQueries.GetScriptFileById(id);

            if (scriptFile == null)
            {
                return NotFound();
            }

            return Ok(scriptFile);
        }

        /// <summary>
        /// Gets a single <c>ScriptParametersFile</c> by its UUID
        /// </summary>
        [HttpGet]
        [Route("scriptparametersfiles/{id:Guid}")]
        public async Task<IActionResult> GetScriptParametersFileById(Guid id)
        {
            var scriptParametersFile = await _analysisProfileExtrasQueries.GetScriptParametersFileById(id);

            if (scriptParametersFile == null)
            {
                return NotFound();
            }

            return Ok(scriptParametersFile);
        }

        /// <summary>
        /// Gets a single <c>MediaType</c> by its UUID
        /// </summary>
        [HttpGet]
        [Route("mediatypes/{id:Guid}")]
        public async Task<IActionResult> GetMediaTypeById(Guid id)
        {
            var mediaType = await _analysisProfileExtrasQueries.GetMediaTypeById(id);

            if (mediaType == null)
            {
                return NotFound();
            }

            return Ok(mediaType);
        }

        /// <summary>
        /// Updates the full content of a <c>ScriptFile</c>
        /// </summary>
        [HttpPut]
        [Route("scriptfile")]
        public async Task<IActionResult> UpdateScriptFile([FromBody] UpdateScriptFileCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Updates the full content of a <c>ScriptParametersFile</c>
        /// </summary>
        [HttpPut]
        [Route("scriptparametersfile")]
        public async Task<IActionResult> UpdateScriptParametersFile([FromBody] UpdateScriptParametersFileCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Updates the full content of a <c>MediaType</c>
        /// </summary>
        [HttpPut]
        [Route("mediatype")]
        public async Task<IActionResult> UpdateMediaType([FromBody] UpdateMediaTypeCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}