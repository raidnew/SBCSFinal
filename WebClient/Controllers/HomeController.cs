using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class HomeController : BaseMyController
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Test()
        {
            return View();
        }
    }
}
