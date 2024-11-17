using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System.Net.Http;
using WebClient.Net;

namespace WebClient.Controllers
{
    public class LandingController : BaseMyController
    {
        [HttpGet]
        [Route("page/{page}")]
        public IActionResult Page()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult AddOrder(Order order)
        {
            ApiConnector.RequestAsync("orders/Add", JsonConvert.SerializeObject(order), HttpMethod.Put);
            return View();
        }
    }
}
