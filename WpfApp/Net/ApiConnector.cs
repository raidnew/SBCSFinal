using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
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

        private readonly string _serverAddress;
        private string _authCookie;
        private string _authJwt;
        private HttpClient Http { get; set; }
        private ApiConnector()
        {
            _serverAddress = "http://localhost:5555";
            Http = new HttpClient();
        }

        public async Task<bool> AuthRequest(AuthenticationRequest request)
        {
            HttpResponseMessage response = Http.PostAsync(
                requestUri: GetUrl("Auth"),
                content: new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8,
                mediaType: "application/json")
            ).Result;
            
            _authJwt = await response.Content.ReadAsStringAsync();

            foreach (var cookieHeader in response.Headers.GetValues("Set-Cookie"))
                _authCookie = cookieHeader;


            return true;
        }

        public async Task<string> RequestAsync(string addr, string content = null, HttpMethod method = null)
        {
            if (method is null) 
                method = HttpMethod.Get;

            Http.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header  
            

            HttpRequestMessage request = new HttpRequestMessage(method, GetUrl(addr));

            if (!(_authJwt is null))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _authJwt);

            if (!(_authCookie is null))
                request.Headers.Add("cookie", _authCookie);

            if (content != null)
                request.Content = new StringContent(content, Encoding.UTF8, "application/json");

            try
            {
                //HttpResponseMessage response = await Http.SendAsync(request);
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
    }
}
