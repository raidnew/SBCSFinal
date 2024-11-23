using API.Models;
using CommonData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebClient.Net;

namespace WebClient.Controllers
{
    public class BaseMyController : Controller
    {
        protected ApiConnector ApiConnector { get; private set; }

        public BaseMyController()
        {
            ApiConnector = new ApiConnector(HttpContext);
        }
        
        public override void OnActionExecuting(ActionExecutingContext ctx)
        {

            ViewBag.menu = JsonConvert.DeserializeObject<IEnumerable<MenuItem>>(ApiConnector.RequestAsync($"Header/GetMenu").Result);
            ViewBag.quote = JsonConvert.DeserializeObject<HeaderText>(ApiConnector.RequestAsync($"Header/GetHeadertext").Result);
        }
    }
}
