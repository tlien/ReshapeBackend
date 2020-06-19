using Microsoft.AspNetCore.Mvc;

namespace Reshape.AccountService.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}