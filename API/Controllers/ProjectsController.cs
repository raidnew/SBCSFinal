using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IEntriesStorage<ProjectEntry> _data;

        public ProjectsController(IEntriesStorage<ProjectEntry> storage)
        {
            _data = storage;
        }

        [HttpGet("GetList")]
        public IEnumerable<ProjectEntry> GetList()
        {
            IEnumerable<ProjectEntry> test = _data.GetAll();
            return test;
        }

        [HttpGet("Get/{id}")]
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