using API.Context;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

        [HttpGet("GetList")]
        public IEnumerable<Order> GetList()
        {
            IEnumerable<Order> test = _ordersData.GetOrders();
            return test;
        }

        [HttpPut("Add")]
        public bool AddOrder(Order order)
        {
            _ordersData.AddOrder(order);
            return true;
        }
    }
}
