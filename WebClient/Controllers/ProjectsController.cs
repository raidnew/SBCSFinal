using API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebClient.Controllers
{
    public class ProjectsController : BaseMyController
    {
        public IActionResult ShowAll()
        {
            ViewBag.projects = JsonConvert.DeserializeObject<IEnumerable<ProjectEntry>>(ApiConnector.RequestAsync("projects/getList").Result);
            return View();
        }
    }
}
