using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System.Net.Http;
using WebClient.Net;

namespace WebClient.Controllers
{
    public class LandingController : Controller
    {
        ApiConnector _apiConnector;

        public LandingController()
        {
            _apiConnector = new ApiConnector(HttpContext);
        }

        [HttpGet]
        [Route("page/{page}")]
        public IActionResult Page()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult AddOrder(Order order)
        {
            _apiConnector.RequestAsync("orders/Add", JsonConvert.SerializeObject(order), HttpMethod.Put);
            return View();
        }
    }
}
