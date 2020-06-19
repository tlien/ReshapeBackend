using Microsoft.AspNetCore.Mvc;

namespace Reshape.BusinessManagementService.API.Controllers
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