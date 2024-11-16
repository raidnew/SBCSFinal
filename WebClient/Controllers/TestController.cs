using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebClient.Controllers
{
    public class TestController : AuthController
    {


        public TestController()
        {


        }

        public async Task<IActionResult> Demo()
        {
           
            return View();
        }

        /*
        public async Task<IActionResult> TestUser()
        {
            return View();
        }
        */

    }
}
