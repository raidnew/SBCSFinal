using API.Models;
using Microsoft.AspNetCore.Authorization;
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
            //ViewBag.orders = JsonConvert.DeserializeObject<IEnumerable<Order>>(ApiConnector.RequestAsync("values").Result);
            //var test = JsonConvert.DeserializeObject<IEnumerable<Order>>(ApiConnector.RequestAsync("values").Result);

            var test = JsonConvert.DeserializeObject<IEnumerable<Order>>(ApiConnector.RequestAsync("orders/getList").Result);
            ViewBag.orders = test;

            return View();
        }

        [HttpGet]
        //[Authorize]
        [Route("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            ViewBag.order = JsonConvert.DeserializeObject<Order>(ApiConnector.RequestAsync($"orders/GetOrder/{id}").Result);
            return View();
        }

        [HttpGet]
        [Authorize]
        [Route("Edit")]
        public IActionResult Edit()
        {
            ViewBag.order = JsonConvert.DeserializeObject<Order>(ApiConnector.RequestAsync($"orders/GetOrder/1").Result);
            return View();
        }

    }
}
