using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;

using Reshape.AccountService.API.Application.Queries.AccountQueries;
using Reshape.AccountService.API.Application.Commands;
using Microsoft.AspNetCore.Authorization;

namespace Reshape.AccountService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAccountQueries _accountQueries;
        public AccountController(IMediator mediator, IAccountQueries accountQueries)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _accountQueries = accountQueries ?? throw new ArgumentNullException(nameof(accountQueries));
        }

        #region Queries
        [HttpGet]
        public async Task<IActionResult> GetAllAccountsAsync()
        {
            return Ok(await _accountQueries.GetAllAccountsAsync());
        }

        [Route("{id:Guid}")]
        [HttpGet]
        public async Task<IActionResult> GetAccountAsync(Guid id)
        {
            return Ok(await _accountQueries.GetAccountById(id));
        }
        #endregion

        #region Commands
        [HttpPost]
        public async Task<IActionResult> CreateAccountAsync([FromBody] CreateAccountCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Route("address")]
        [HttpPut]
        public async Task<IActionResult> SetAddress([FromBody] SetAddressCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Route("contactdetails")]
        [HttpPut]
        public async Task<IActionResult> SetContactDetails([FromBody] SetContactDetailsCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Route("businesstier")]
        [HttpPut]
        public async Task<IActionResult> SetBusinessTier([FromBody] SetBusinessTierCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Route("activate")]
        [HttpPut]
        public async Task<IActionResult> ActivateAccount([FromBody] ActivateAccountCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Route("deactivate")]
        [HttpPut]
        public async Task<IActionResult> DeactivateAccount([FromBody] DeactivateAccountCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Route("features/add")]
        [HttpPut]
        public async Task<IActionResult> AddFeatures([FromBody] AddFeaturesCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Route("analysisprofiles/add")]
        [HttpPut]
        public async Task<IActionResult> AddAnalysisProfiles([FromBody] AddAnalysisProfilesCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Route("features/remove")]
        [HttpPut]
        public async Task<IActionResult> RemoveFeatures([FromBody] RemoveFeaturesCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Route("analysisprofiles/remove")]
        [HttpPut]
        public async Task<IActionResult> RemoveAnalysisProfiles([FromBody] RemoveAnalysisProfilesCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        #endregion
    }
}