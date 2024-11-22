using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class ProjectsController : Controller
    {
        [Authorize]
        public IActionResult ShowAll()
        {
            return View();
        }
    }
}
