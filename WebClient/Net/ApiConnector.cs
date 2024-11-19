using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using WebClient.Auth;
using static System.Net.WebRequestMethods;

namespace WebClient.Net
{
    public class ApiConnector
    {
        private readonly string _serverAddress;
        private HttpClient Http { get; set; }
        private static HttpContext _httpContext => new HttpContextAccessor().HttpContext;
        public ApiConnector(HttpContext httpContext)
        {
            //_serverAddress = "http://localhost:6666";
            //_serverAddress = "http://localhost:50379";
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

            foreach (var cookieHeader in response.Headers.GetValues("Set-Cookie"))
                _httpContext.Session.SetString("cookie", cookieHeader);

            _httpContext.Session.SetString("token", jwt);

            /*
            Uri uri = new Uri(_serverAddress);
            var cookieJar = new CookieContainer();
            var responseCookies = cookieJar.GetCookies(uri);
            foreach (Cookie cookie in responseCookies)
            {
                string cookieName = cookie.Name;
                string cookieValue = cookie.Value;
            }
            */

            /*
            foreach (var cookieHeader in response.Headers.GetValues("Set-Cookie"))
            {

            }
                //cookies.SetCookies(new Uri(_serverAddress), cookieHeader);
            */
            return jwt;
        }

        public async Task<string> RequestAsync(string addr, string content = null, HttpMethod method = null)
        {
            if (method is null) 
                method = HttpMethod.Get;

            Http.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header  
            

            HttpRequestMessage request = new HttpRequestMessage(method, GetUrl(addr));
            string jwt = _httpContext.Session.GetString("token");

            if (!(jwt is null))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer",jwt);

            string authCookie = _httpContext.Session.GetString("cookie");
            if (!(authCookie is null))
                request.Headers.Add("cookie", authCookie);

            if (content != null)
                request.Content = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await Http.SendAsync(request);

            return await response.Content.ReadAsStringAsync();
        }

        private string GetUrl(string action)
        {
            return $"{_serverAddress}/api/{action}";
        }
    }
}
