using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebClient.Auth;

namespace WebClient.Controllers
{
    public class AccountController : Controller
    {
        //private AuthConnector _authConnector;
        private readonly string _serverAddress;
        private HttpClient HttpClient { get; set; }

        public AccountController()
        {
            _serverAddress = "http://localhost:5555";
            HttpClient = new HttpClient();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new UserLoginData() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginData authData)
        {

            AuthenticationRequest requrest = new AuthenticationRequest();
            requrest.Name = authData.UserName;
            requrest.Password = authData.Password;

            HttpResponseMessage response = HttpClient.PostAsync(
                requestUri: GetUrl("Auth"),
                content: new StringContent(JsonConvert.SerializeObject(requrest), Encoding.UTF8,
                mediaType: "application/json")
            ).Result;

            string jwt = await response.Content.ReadAsStringAsync();
            HttpContext.Session.SetString("token", jwt);

            return View(authData);
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> Logout()
        {
            return RedirectToMainPage();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new UserRegistrationData());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegistrationData model)
        {
            return View(model);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        private RedirectToActionResult RedirectToMainPage()
        {
            return RedirectToAction("ContactsList", "PhoneBook");
        }

        private string GetUrl(string action)
        {
            return $"{_serverAddress}/api/{action}";
        }
    }
}
