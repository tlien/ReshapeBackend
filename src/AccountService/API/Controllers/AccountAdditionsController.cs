using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Reshape.AccountService.API.Application.Queries.AccountAdditionsQueries;

namespace Reshape.AccountService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
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
            return Ok(await _accountAdditionsQueries.GetAllFeaturesAsync());
        }

        [Route("features/{id:Guid}")]
        [HttpGet]
        public async Task<IActionResult> GetFeatureAsync(Guid featureId)
        {
            return Ok(await _accountAdditionsQueries.GetFeatureById(featureId));
        }

        [Route("businesstiers")]
        [HttpGet]
        public async Task<IActionResult> GetAllBusinessTiersAsync()
        {
            return Ok(await _accountAdditionsQueries.GetAllBusinessTiersAsync());
        }

        [Route("businesstiers/{id:Guid}")]
        [HttpGet]
        public async Task<IActionResult> GetBusinessTierAsync(Guid businessTierId)
        {
            return Ok(await _accountAdditionsQueries.GetBusinessTierById(businessTierId));
        }

        [Route("analysisprofiles")]
        [HttpGet]
        public async Task<IActionResult> GetAllAnalysisProfilesAsync()
        {
            return Ok(await _accountAdditionsQueries.GetAllAnalysisProfilesAsync());
        }

        [Route("analysisprofiles/{id:Guid}")]
        [HttpGet]
        public async Task<IActionResult> GetAnalysisProfileAsync(Guid analysisProfilesId)
        {
            return Ok(await _accountAdditionsQueries.GetAnalysisProfileById(analysisProfilesId));
        }
    }
}