using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IEntriesStorage<ServiceEntry> _data;

        public ServicesController(IEntriesStorage<ServiceEntry> storage)
        {
            _data = storage;
        }

        [HttpGet("GetList")]
        public IEnumerable<ServiceEntry> GetList()
        {
            IEnumerable<ServiceEntry> test = _data.GetAll();
            return test;
        }

        [HttpGet("GetOrder/{id}")]
        [Authorize]
        public ServiceEntry GetOrder(int id)
        {
            ServiceEntry ret = _data.GetById(id);
            return ret;
        }

        [HttpPut("Add")]
        [Authorize]
        public bool AddOrder(ServiceEntry order)
        {
            _data.Add(order);
            return true;
        }
    }
}
