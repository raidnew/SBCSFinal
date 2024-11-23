using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IEntriesStorage<ProjectEntry> _data;

        public ProjectController(IEntriesStorage<ProjectEntry> storage)
        {
            _data = storage;
        }

        [HttpGet("GetList")]
        public IEnumerable<ProjectEntry> GetList()
        {
            IEnumerable<ProjectEntry> test = _data.GetAll();
            return test;
        }

        [HttpGet("GetOrder/{id}")]
        [Authorize]
        public ProjectEntry GetOrder(int id)
        {
            ProjectEntry ret = _data.GetById(id);
            return ret;
        }

        [HttpPut("Add")]
        [Authorize]
        public bool AddOrder(ProjectEntry order)
        {
            _data.Add(order);
            return true;
        }
    }
}