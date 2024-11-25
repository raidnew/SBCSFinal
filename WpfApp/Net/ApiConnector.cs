using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebClient.Auth;

namespace WebClient.Net
{
    public class ApiConnector
    {
        private static ApiConnector _instance;

        public static ApiConnector Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ApiConnector();
                return _instance;
            }
        }

        public Action HasLogged;
        private readonly string _serverAddress;
        private string _authCookie;
        private string _authJwt;
        private HttpClient Http { get; set; }
        private ApiConnector()
        {
            _serverAddress = "http://localhost:5555";
            Http = new HttpClient();
        }

        public String GetLoggedName()
        {
            if (_authJwt == null) return null;

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(_authJwt);
            string name = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name).Value.ToString();
            return name;
        }

        public async Task<bool> AuthRequest(AuthenticationRequest request)
        {
            HttpResponseMessage response = Http.PostAsync(
                requestUri: GetUrl("Auth"),
                content: new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8,
                mediaType: "application/json")
            ).Result;

            _authJwt = await response.Content.ReadAsStringAsync();
            _authJwt = _authJwt.Trim('"');

            foreach (var cookieHeader in response.Headers.GetValues("Set-Cookie"))
                _authCookie = cookieHeader;

            if (_authJwt != null)
            {
                HasLogged?.Invoke();
                return true;
            }
            return false;
        }

        public async Task<string> RequestAsync(string addr, string content = null, HttpMethod method = null)
        {
            if (method is null) 
                method = HttpMethod.Get;

            Http.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            

            HttpRequestMessage request = new HttpRequestMessage(method, GetUrl(addr));

            if (!(_authJwt is null))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _authJwt);

            if (!(_authCookie is null))
                request.Headers.Add("cookie", _authCookie);

            if (content != null)
                request.Content = new StringContent(content, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await Http.SendAsync(request);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                var test = ex.Message;
                Trace.WriteLine(test);
            }
            return "";
        }

        private string GetUrl(string action)
        {
            return $"{_serverAddress}/api/{action}";
        }

        internal void Logout()
        {
            _authJwt = null;
            _authCookie = null;
            HasLogged?.Invoke();
        }
    }
}
