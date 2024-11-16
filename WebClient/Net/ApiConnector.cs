using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WebClient.Auth;

namespace WebClient.Net
{
    public class ApiConnector : Controller
    {
        private readonly string _serverAddress;
        private HttpClient Http { get; set; }

        public ApiConnector()
        {
            _serverAddress = "http://localhost:5555";
            Http = new HttpClient();
        }

        public async Task<string> AuthRequest(AuthenticationRequest request)
        {
            HttpResponseMessage response = Http.PostAsync(
                requestUri: GetUrl("Auth"),
                content: new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8,
                mediaType: "application/json")
            ).Result;

            string jwt = await response.Content.ReadAsStringAsync();
            return jwt;
        }

        public async Task<string> RequestAsync(string addr)
        {
            string jwt = HttpContext.Session.GetString("token");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, GetUrl(addr));
            request.Headers.Authorization = new AuthenticationHeaderValue(jwt);
            HttpResponseMessage response = await Http.SendAsync(request);
            string data = await response.Content.ReadAsStringAsync();
            return data;
        }

        private string GetUrl(string action)
        {
            return $"{_serverAddress}/api/{action}";
        }
    }
}
