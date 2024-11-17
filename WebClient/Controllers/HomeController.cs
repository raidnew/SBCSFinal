using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseMyController
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Test")]
        public string Test()
        {
            return "Test";
        }

        [HttpGet]
        [Route("Test2")]
        public string Test2()
        {
            return "Test2";
        }
    }
}
