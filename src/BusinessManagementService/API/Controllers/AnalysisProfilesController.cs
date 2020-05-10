using System;
using System.Net;
using System.Threading.Tasks;
using BusinessManagementService.API.Application.Commands;
using BusinessManagementService.API.Application.Queries.AnalysisProfileQueries;
using BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static BusinessManagementService.API.Application.Commands.CreateAnalysisProfileCommandHandler;

namespace BusinessManagementService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AnalysisProfilesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAnalysisProfileQueries _analysisProfileQueries;

        public AnalysisProfilesController(IMediator mediator, IAnalysisProfileQueries analysisProfileQueries)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _analysisProfileQueries = analysisProfileQueries;
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
            return Ok(await _analysisProfileQueries.GetById(id));
        }

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