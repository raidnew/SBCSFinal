using API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebClient.Controllers
{
    public class ContactsController : BaseMyController
    {
        public IActionResult ShowAll()
        {
            var data = ApiConnector.RequestAsync("contacts/GetList").Result;

            var test = JsonConvert.DeserializeObject<IEnumerable<ContactEntry>>(data);
            ViewBag.contacts = test;

            return View();
        }
    }
}
