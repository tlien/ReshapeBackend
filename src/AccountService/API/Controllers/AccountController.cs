using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;

using Reshape.AccountService.API.Application.Queries.AccountQueries;
using Reshape.AccountService.API.Application.Commands;

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
        /// <summary>
        /// Gets all Accounts
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "admin")]
        // [Authorize(Roles = "accountAdmin")]
        public async Task<IActionResult> GetAllAccountsAsync()
        {
            return Ok(await _accountQueries.GetAllAccountsAsync());
        }

        /// <summary>
        /// Gets a single Account by its UUID
        /// </summary>
        [Route("{id:Guid}")]
        [HttpGet]
        [Authorize(Roles = "admin")]
        // [Authorize(Roles = "accountAdmin")]
        public async Task<IActionResult> GetAccountAsync(Guid id)
        {
            return Ok(await _accountQueries.GetAccountById(id));
        }

        /// <summary>
        /// Gets the Account of the user that is currently logged in
        /// </summary>
        [Route("own")]
        [HttpGet]
        public async Task<IActionResult> GetUserAssociatedAccountAsync()
        {
            var ownId = User.Claims.SingleOrDefault(x => x.Type == "sub").Value;
            return Ok(await _accountQueries.GetAccountById(Guid.Parse(ownId)));
        }
        #endregion

        #region Commands
        /// <summary>
        /// Creates a new Account
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateAccountAsync([FromBody] CreateAccountCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Sets the Address of an Account
        /// </summary>
        [Route("address")]
        [HttpPut]
        [Authorize(Roles = "admin")]
        // [Authorize(Roles = "accountAdmin")]
        public async Task<IActionResult> SetAddress([FromBody] SetAddressCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Sets the ContactDetails of an Account
        /// </summary>
        [Route("contactdetails")]
        [HttpPut]
        [Authorize(Roles = "admin")]
        // [Authorize(Roles = "accountAdmin")]
        public async Task<IActionResult> SetContactDetails([FromBody] SetContactDetailsCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Sets the BusinessTier relation of an Account
        /// </summary>
        [Route("businesstier")]
        [HttpPut]
        [Authorize(Roles = "admin")]
        // [Authorize(Roles = "accountAdmin")]
        public async Task<IActionResult> SetBusinessTier([FromBody] SetBusinessTierCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Sets the Active property of an Account to 'true', activating the Account
        /// </summary>
        [Route("activate")]
        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ActivateAccount([FromBody] ActivateAccountCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Sets the Active property of an Account to 'false', deactivating the Account
        /// </summary>
        [Route("deactivate")]
        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeactivateAccount([FromBody] DeactivateAccountCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Adds a list of Feature relations to an Account
        /// </summary>
        [Route("features/add")]
        [HttpPut]
        [Authorize(Roles = "admin")]
        // [Authorize(Roles = "accountAdmin")]
        public async Task<IActionResult> AddFeatures([FromBody] AddFeaturesCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Adds a list of AnalysisProfile relations to an Account
        /// </summary>
        [Route("analysisprofiles/add")]
        [HttpPut]
        [Authorize(Roles = "admin")]
        // [Authorize(Roles = "accountAdmin")]
        public async Task<IActionResult> AddAnalysisProfiles([FromBody] AddAnalysisProfilesCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Removes a list of Feature relations from an Account
        /// </summary>
        [Route("features/remove")]
        [HttpPut]
        [Authorize(Roles = "admin")]
        // [Authorize(Roles = "accountAdmin")]
        public async Task<IActionResult> RemoveFeatures([FromBody] RemoveFeaturesCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Removes a list of AnalysisProfile relations from an Account
        /// </summary>
        [Route("analysisprofiles/remove")]
        [HttpPut]
        [Authorize(Roles = "admin")]
        // [Authorize(Roles = "accountAdmin")]
        public async Task<IActionResult> RemoveAnalysisProfiles([FromBody] RemoveAnalysisProfilesCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        #endregion
    }
}