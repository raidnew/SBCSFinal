using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using WebClient.Auth;

namespace WebClient.Net
{
    public class ApiConnector
    {
        private readonly string _serverAddress;
        private HttpClient Http { get; set; }
        private static HttpContext _httpContext => new HttpContextAccessor().HttpContext;
        public ApiConnector(HttpContext httpContext)
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

        public async Task<HttpResponseMessage> RequestAsync(string addr, string content = null, HttpMethod method = null)
        {
            if (method is null) 
                method = HttpMethod.Get;
            HttpRequestMessage request = new HttpRequestMessage(method, GetUrl(addr));
            string jwt = _httpContext.Session.GetString("token");
            if (!(jwt is null))
                request.Headers.Authorization = new AuthenticationHeaderValue(jwt);

            if (content != null)
                request.Content = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await Http.SendAsync(request);
            //HttpResponseMessage response = await Http.SendAsync(request).Result;
            //T ret = (T)await response.Content.ReadAsStreamAsync;
            return response;
        }

        private string GetUrl(string action)
        {
            return $"{_serverAddress}/api/{action}";
        }
    }
}
