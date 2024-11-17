using API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using WebClient.Net;

namespace WebClient.Controllers
{
    public class OrdersController : BaseMyController
    {
        public IActionResult ShowAll()
        {
            var test = ApiConnector.RequestAsync("orders/getList").Result;

            //ViewBag.orders = ApiConnector.RequestAsync("orders/getList").Result;

            //HttpResponseMessage response = await Http.SendAsync(request).Result;
            //T ret = (T)await response.Content.ReadAsStreamAsync;


            return View();
        }
    }
}
