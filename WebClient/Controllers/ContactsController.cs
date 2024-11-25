using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebClient.Controllers
{
    [Route("[controller]")]
    public class ContactsController : BaseMyController
    {
        [HttpGet]
        [Route("ShowAll")]
        public IActionResult ShowAll()
        {
            ViewBag.contacts = JsonConvert.DeserializeObject<IEnumerable<ContactEntry>>(ApiConnector.RequestAsync("contacts/GetList").Result);
            return View();
        }
    }
}
