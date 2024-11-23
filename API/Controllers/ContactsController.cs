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
    public class ContactsController : ControllerBase
    {
        private readonly IEntriesStorage<ContactEntry> _data;

        public ContactsController(IEntriesStorage<ContactEntry> storage)
        {
            _data = storage;
        }

        [HttpGet("GetList")]
        public IEnumerable<ContactEntry> GetList()
        {
            IEnumerable<ContactEntry> test = _data.GetAll();
            return test;
        }

        [HttpGet("GetOrder/{id}")]
        [Authorize]
        public ContactEntry GetOrder(int id)
        {
            ContactEntry ret = _data.GetById(id);
            return ret;
        }

        [HttpPut("Add")]
        [Authorize]
        public bool AddOrder(ContactEntry order)
        {
            _data.Add(order);
            return true;
        }
    }


}
