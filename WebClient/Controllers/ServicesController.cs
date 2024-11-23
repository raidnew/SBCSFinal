using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult ShowAll()
        {
            return View();
        }
    }
}
