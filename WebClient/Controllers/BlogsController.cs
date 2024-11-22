using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class BlogsController : Controller
    {
        public IActionResult ShowAll()
        {
            return View();
        }
    }
}
