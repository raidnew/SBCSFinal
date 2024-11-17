using API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using WebClient.Net;

namespace WebClient.Controllers
{
    public class OrdersController : BaseMyController
    {
        public IActionResult ShowAll()
        {
            ViewBag.orders = JsonConvert.DeserializeObject<IEnumerable<Order>>(ApiConnector.RequestAsync("orders/getList").Result);

            return View();
        }
    }
}
