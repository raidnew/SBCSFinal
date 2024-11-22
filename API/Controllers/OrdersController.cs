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
        private readonly IEntriesStorage<Order> _ordersData;

        public OrdersController(IEntriesStorage<Order> orders)
        {
            _ordersData = orders;
        }

        [HttpGet("GetList")]
        public IEnumerable<Order> GetList()
        {
            IEnumerable<Order> test = _ordersData.GetAll();
            return test;
        }

        [HttpGet("GetOrder/{id}")]
        [Authorize]
        public Order GetOrder(int id)
        {
            Order ret = _ordersData.GetById(id);
            return ret;
        }

        [HttpPut("Add")]
        [Authorize]
        public bool AddOrder(Order order)
        {
            _ordersData.Add(order);
            return true;
        }
    }
}
