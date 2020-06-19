using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reshape.BusinessManagementService.API.Application.Commands;
using Reshape.BusinessManagementService.API.Application.Queries.AnalysisProfileExtrasQueries;

namespace Reshape.BusinessManagementService.API.Controllers
{
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

        [HttpGet]
        [Route("scriptfiles")]
        public async Task<IActionResult> GetScriptFiles()
        {
            return Ok(await _analysisProfileExtrasQueries.GetScriptFiles());
        }

        [HttpGet]
        [Route("scriptparametersfiles")]
        public async Task<IActionResult> GetScriptParametersFiles()
        {
            return Ok(await _analysisProfileExtrasQueries.GetScriptParametersFiles());
        }

        [HttpGet]
        [Route("mediatypes")]
        public async Task<IActionResult> GetMediaTypes()
        {
            return Ok(await _analysisProfileExtrasQueries.GetMediaTypes());
        }

        [HttpGet]
        [Route("scriptfiles/{id:Guid}")]
        public async Task<IActionResult> GetScriptFileById(Guid id)
        {
            return Ok(await _analysisProfileExtrasQueries.GetScriptFileById(id));
        }

        [HttpGet]
        [Route("scriptparametersfiles/{id:Guid}")]
        public async Task<IActionResult> GetScriptParameterssFileById(Guid id)
        {
            return Ok(await _analysisProfileExtrasQueries.GetScriptParametersFileById(id));
        }

        [HttpGet]
        [Route("mediatypes/{id:Guid}")]
        public async Task<IActionResult> GetMediaTypeById(Guid id)
        {
            return Ok(await _analysisProfileExtrasQueries.GetMediaTypeById(id));
        }

        [HttpPut]
        [Route("scriptfile")]
        public async Task<IActionResult> UpdateScriptFile([FromBody] UpdateScriptFileCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut]
        [Route("scriptparametersfile")]
        public async Task<IActionResult> UpdateScriptParametersFile([FromBody] UpdateScriptParametersFileCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut]
        [Route("mediatype")]
        public async Task<IActionResult> UpdateMediaType([FromBody] UpdateMediaTypeCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}