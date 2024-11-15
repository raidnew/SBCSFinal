using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebClient.Controllers
{
    public class TestController : Controller
    {
        private readonly string _serverAddress;
        private HttpClient Http { get; set; }

        public TestController()
        {
            _serverAddress = "http://localhost:5555";
            Http = new HttpClient();
        }

        public async Task<string> TestAsync()
        {
            string jwt = HttpContext.Session.GetString("token");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, GetUrl("Test/Data"));
            request.Headers.Authorization = new AuthenticationHeaderValue(jwt);

            HttpResponseMessage response = await Http.SendAsync(request);
            string data = await response.Content.ReadAsStringAsync();
            /*

            //Uri target = new Uri(_serverAddress);
            string jwt = HttpContext.Session.GetString("token");
            HttpContext.Request.Headers.Add(HeaderNames.Authorization, jwt);

            HttpClient client = new HttpClient();

            HttpResponseMessage response = client.GetAsync(
                requestUri: GetUrl("Test/Data")
            ).Result;

            string data = await response.Content.ReadAsStringAsync();
            */
            return ">" + data + "<";
        }

        private string GetUrl(string action)
        {
            return $"{_serverAddress}/api/{action}";
        }
    }
}
