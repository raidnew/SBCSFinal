using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebClient.Controllers
{
    public class TestController : BaseMyController
    {
        private readonly string _serverAddress;
        private HttpClient Http { get; set; }

        public TestController()
        {
            _serverAddress = "http://localhost:5555";
            Http = new HttpClient();
        }

        //[AllowAnonymous]
        public string Demo2()
        {
            
            return "Demo2";
        }

        //[Authorize(Policy = "user")]
        //[Authorize(Policy = "customauth")]
        public string Demo1()
        {
            return "Demo1";
        }


        public async Task<IActionResult> Demo()
        {
            string jwt = HttpContext.Session.GetString("token");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, GetUrl("Test/Data"));
            request.Headers.Authorization = new AuthenticationHeaderValue(jwt);

            HttpResponseMessage response = await Http.SendAsync(request);
            string data = await response.Content.ReadAsStringAsync();

            return View();
        }

        /*
        public async Task<IActionResult> TestUser()
        {
            return View();
        }
        */

        private string GetUrl(string action)
        {
            return $"{_serverAddress}/api/{action}";
        }
    }
}
