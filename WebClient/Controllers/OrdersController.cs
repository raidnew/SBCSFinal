using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
            var orders = JsonConvert.DeserializeObject<IEnumerable<Order>>(ApiConnector.RequestAsync("orders/getList").Result);
            var statuses = JsonConvert.DeserializeObject<IEnumerable<OrdersStatus>>(ApiConnector.RequestAsync($"orders/GetStatuses").Result);

            ViewBag.orders = orders;
            ViewBag.statuses = statuses;

            return View();
        }

        [HttpGet]
        [Authorize]
        [Route("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var order = JsonConvert.DeserializeObject<Order>(ApiConnector.RequestAsync($"orders/GetOrder/{id}").Result);
            var statuses = JsonConvert.DeserializeObject<IEnumerable<OrdersStatus>>(ApiConnector.RequestAsync($"orders/GetStatuses").Result);

            ViewBag.order = order;
            ViewBag.statuses = statuses;
            ViewBag.currentStatus = statuses.FirstOrDefault<OrdersStatus>(status => status.Id == order.StatusId);

            return View();
        }

        [HttpPost]
        [Authorize]
        [Route("Edit")]
        public IActionResult Edit(int orderId, int newStateId)
        {
            var order = JsonConvert.DeserializeObject<Order>(ApiConnector.RequestAsync($"orders/GetOrder/{orderId}").Result);
            order.StatusId = newStateId;

            Task task = ApiConnector.RequestAsync("orders/Edit", JsonConvert.SerializeObject(order), HttpMethod.Post);
            task.Wait();

            return Redirect($"/Orders/Edit/{orderId}");
        }

    }
}
