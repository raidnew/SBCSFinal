using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using WebClient.Auth;

namespace WebClient.Net
{
    public class AuthConnector
    {
        private readonly string _serverAddress;
        private HttpClient HttpClient { get; set; }

        public AuthConnector()
        {
            _serverAddress = "http://localhost:5555";
            HttpClient = new HttpClient();
        }
        public HttpResponseMessage Login(UserLoginData authData)
        {
            AuthenticationRequest requrest = new AuthenticationRequest();
            requrest.Name = authData.UserName;
            requrest.Password = authData.Password;

            HttpResponseMessage message = HttpClient.PostAsync(
                requestUri: GetUrl("Auth"),
                content: new StringContent(JsonConvert.SerializeObject(requrest), Encoding.UTF8,
                mediaType: "application/json")
            ).Result;

            return message;
        }

        private string GetUrl(string action)
        {
            return $"{_serverAddress}/api/{action}";
        }
    }
}
