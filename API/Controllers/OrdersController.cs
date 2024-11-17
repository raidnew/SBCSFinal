using API.Context;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersEntries _ordersData;

        public OrdersController(IOrdersEntries orders)
        {
            _ordersData = orders;
        }

        [HttpPut("Add")]
        [AllowAnonymous]
        public bool AddOrder(Order order)
        {
            _ordersData.AddOrder(order);
            return true;
        }
    }
}
