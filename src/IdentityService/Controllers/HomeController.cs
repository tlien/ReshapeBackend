using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Reshape.IdentityService.Infrastructure;

namespace Reshape.IdentityService.Controllers
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index() => RedirectToAction("login", "Account");
    }
}
