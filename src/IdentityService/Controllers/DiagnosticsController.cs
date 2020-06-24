using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Reshape.IdentityService.Infrastructure;
using Reshape.IdentityService.Models;

namespace Reshape.IdentityService.Controllers
{
    /// <summary>
    /// This controller serves the Diagnostics page showing the contents of the auth cookie of the currently signed in user.
    /// It is NOT set up to show all available information at this point.
    /// </summary>
    [SecurityHeaders]
    [Authorize]
    public class DiagnosticsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var model = new DiagnosticsViewModel(await HttpContext.AuthenticateAsync());
            return View(model);
        }
    }
}