using Microsoft.AspNetCore.Mvc;

namespace Reshape.AccountService.API.Controllers
{
    [ApiController]
    [Route("/")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}