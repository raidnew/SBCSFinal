using API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using WebClient.Net;

namespace WebClient.Controllers
{
    [Route("[controller]")]
    public class OrdersController : BaseMyController
    {
        [HttpGet]
        [Route("ShowAll")]
        public IActionResult ShowAll()
        {
            ViewBag.orders = JsonConvert.DeserializeObject<IEnumerable<Order>>(ApiConnector.RequestAsync("orders/getList").Result);

            return View();
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            ViewBag.order = JsonConvert.DeserializeObject<Order>(ApiConnector.RequestAsync($"orders/GetOrder/{id}").Result);
            return View();
        }
    }
}
