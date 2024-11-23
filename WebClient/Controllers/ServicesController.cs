using API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebClient.Controllers
{
    public class ServicesController : BaseMyController
    {
        public IActionResult ShowAll()
        {
            ViewBag.services = JsonConvert.DeserializeObject<IEnumerable<ServiceEntry>>(ApiConnector.RequestAsync("services/getList").Result);
            return View();
        }
    }
}
