using System;
using System.Threading.Tasks;
using BusinessManagementService.API.Application.Commands;
using BusinessManagementService.API.Application.Queries.BusinessTierQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagementService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BusinessTiersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IBusinessTierQueries _businessTierQueries;

        public BusinessTiersController(IMediator mediator, IBusinessTierQueries businessTierQueries)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _businessTierQueries = businessTierQueries ?? throw new ArgumentNullException(nameof(businessTierQueries));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _businessTierQueries.GetAllAsync());
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            return Ok(await _businessTierQueries.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateBusinessTierCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}