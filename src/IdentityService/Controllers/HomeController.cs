using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using IdentityServer4.Services;

using Reshape.IdentityService.Infrastructure;
using Reshape.IdentityService.Models;

namespace Reshape.IdentityService.Controllers
{
    /// <summary>
    /// This is the home controller. Depending on whether a user is logged in on the current device/browser, it will either redirect to the login page or diagnostics page.
    /// The diagnostics page will only be shown while in development.
    /// </summary>
    [SecurityHeaders]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger _logger;

        public HomeController(IIdentityServerInteractionService interaction, IWebHostEnvironment environment, ILogger<HomeController> logger)
        {
            _interaction = interaction;
            _environment = environment;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("login", "Account");

            if (_environment.IsDevelopment())
            {
                // DEV: only show in development
                return RedirectToAction("index", "Diagnostics");
            }

            _logger.LogInformation("Homepage is disabled in production. Returning 404.");
            return NotFound();
        }

        /// <summary>
        /// Shows the error page
        /// </summary>
        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();

            // retrieve error details from identityserver
            var message = await _interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                vm.Error = message;

                if (!_environment.IsDevelopment())
                {
                    // only show in development
                    message.ErrorDescription = null;
                }
            }

            return View("Error", vm);
        }
    }
}
