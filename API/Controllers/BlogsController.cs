using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IEntriesStorage<BlogEntry> _data;

        public BlogsController(IEntriesStorage<BlogEntry> storage)
        {
            _data = storage;
        }

        [HttpGet("GetList")]
        public IEnumerable<BlogEntry> GetList()
        {
            IEnumerable<BlogEntry> test = _data.GetAll();
            return test;
        }

        [HttpGet("GetBlog/{id}")]
        public BlogEntry GetBlog(int id)
        {
            BlogEntry ret = _data.GetById(id);
            return ret;
        }

        [HttpPut("Add")]
        [Authorize]
        public bool Add(BlogEntry obj)
        {
            _data.Add(obj);
            return true;
        }
    }


}
