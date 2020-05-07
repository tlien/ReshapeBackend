using System;
using System.Net;
using System.Threading.Tasks;
using BusinessManagementService.API.Application.Commands.AnalysisProfileCommands;
using BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static BusinessManagementService.API.Application.Commands.AnalysisProfileCommands.CreateAnalysisProfileCommandHandler;

namespace BusinessManagementService.API 
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AnalysisProfilesController : ControllerBase
    {
        private readonly IMediator _mediator;
        // private readonly IAnalysisProfileQueries _analysisProfileQueries;

        public AnalysisProfilesController(IMediator mediator) 
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public IActionResult GetAllAsync(Guid id) 
        {
            return Ok("Det virker!!");
        }

        // [Route("{analysisProfileId:Guid}")]
        // [HttpGet]
        // public Task<IActionResult> GetAsync(Guid id) 
        // {
        //     return Ok();
        // }

        [Route("add")]
        [HttpPost]
        public async Task<ActionResult<AnalysisProfileDTO>> AddAsync([FromBody] CreateAnalysisProfileCommand command)
        {
            return await _mediator.Send(command);
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