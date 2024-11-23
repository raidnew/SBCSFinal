using API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using WebClient.Net;

namespace WebClient.Controllers
{
    [Route("[controller]")]
    public class BlogsController : BaseMyController
    {
        [Route("ShowAll")]
        public IActionResult ShowAll()
        {
            ViewBag.blogs = JsonConvert.DeserializeObject<IEnumerable<BlogEntry>>(ApiConnector.RequestAsync("blogs/getList").Result);
            return View();
        }

        [Route("View/{id}")]
        public IActionResult View(int id)
        {

            var addr = $"Blogs/GetBlog/{id}";
            var respond = ApiConnector.RequestAsync(addr).Result;

            ViewBag.blog = JsonConvert.DeserializeObject<BlogEntry>(respond);
            return View();
        }
    }
}
