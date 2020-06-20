using System;
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

        /// <summary>
        /// Gets all Features
        /// </summary>
        [Route("features")]
        [HttpGet]
        public async Task<IActionResult> GetAllFeaturesAsync()
        {
            return Ok(await _accountAdditionsQueries.GetAllFeaturesAsync());
        }

        /// <summary>
        /// Gets a single Feature by its UUID
        /// </summary>
        [Route("features/{id:Guid}")]
        [HttpGet]
        public async Task<IActionResult> GetFeatureAsync(Guid id)
        {
            return Ok(await _accountAdditionsQueries.GetFeatureById(id));
        }

        /// <summary>
        /// Gets all BusinessTiers
        /// </summary>
        [Route("businesstiers")]
        [HttpGet]
        public async Task<IActionResult> GetAllBusinessTiersAsync()
        {
            return Ok(await _accountAdditionsQueries.GetAllBusinessTiersAsync());
        }

        /// <summary>
        /// Gets a single BusinessTier by its UUID
        /// </summary>
        [Route("businesstiers/{id:Guid}")]
        [HttpGet]
        public async Task<IActionResult> GetBusinessTierAsync(Guid id)
        {
            return Ok(await _accountAdditionsQueries.GetBusinessTierById(id));
        }

        /// <summary>
        /// Gets all AnalysisProfiles
        /// </summary>
        [Route("analysisprofiles")]
        [HttpGet]
        public async Task<IActionResult> GetAllAnalysisProfilesAsync()
        {
            return Ok(await _accountAdditionsQueries.GetAllAnalysisProfilesAsync());
        }

        /// <summary>
        /// Gets a single AnalysisProfile by its UUID
        /// </summary>
        [Route("analysisprofiles/{id:Guid}")]
        [HttpGet]
        public async Task<IActionResult> GetAnalysisProfileAsync(Guid id)
        {
            return Ok(await _accountAdditionsQueries.GetAnalysisProfileById(id));
        }
    }
}