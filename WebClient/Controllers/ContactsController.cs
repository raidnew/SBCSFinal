using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult ShowAll()
        {

            return View();
        }
    }
}
