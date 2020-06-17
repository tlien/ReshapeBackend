using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Reshape.AccountService.API.Application.Queries.AccountAdditionsQueries;

namespace Reshape.AccountService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountAdditionsController : ControllerBase
    {
        private readonly IAccountAdditionsQueries _accountAdditionsQueries;

        public AccountAdditionsController(IAccountAdditionsQueries accountAdditionsQueries)
        {
            _accountAdditionsQueries = accountAdditionsQueries ?? throw new ArgumentNullException(nameof(accountAdditionsQueries));
        }

        [Route("features")]
        [HttpGet]
        public async Task<IActionResult> GetAllFeaturesAsync()
        {
            var user = HttpContext.User as ClaimsPrincipal;
            return Ok(await _accountAdditionsQueries.GetAllFeaturesAsync());
        }

        [Route("features/{id:Guid}")]
        [HttpGet]
        public async Task<IActionResult> GetFeatureAsync(Guid id)
        {
            return Ok(await _accountAdditionsQueries.GetFeatureById(id));
        }

        [Route("businesstiers")]
        [HttpGet]
        public async Task<IActionResult> GetAllBusinessTiersAsync()
        {
            return Ok(await _accountAdditionsQueries.GetAllBusinessTiersAsync());
        }

        [Route("businesstiers/{id:Guid}")]
        [HttpGet]
        public async Task<IActionResult> GetBusinessTierAsync(Guid id)
        {
            return Ok(await _accountAdditionsQueries.GetBusinessTierById(id));
        }

        [Route("analysisprofiles")]
        [HttpGet]
        public async Task<IActionResult> GetAllAnalysisProfilesAsync()
        {
            return Ok(await _accountAdditionsQueries.GetAllAnalysisProfilesAsync());
        }

        [Route("analysisprofiles/{id:Guid}")]
        [HttpGet]
        public async Task<IActionResult> GetAnalysisProfileAsync(Guid id)
        {
            return Ok(await _accountAdditionsQueries.GetAnalysisProfileById(id));
        }
    }
}