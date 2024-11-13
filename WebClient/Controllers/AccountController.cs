using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using WebClient.Auth;
using WebClient.Models;
using WebClient.Net;

namespace WebClient.Controllers
{
    public class AccountController : Controller
    {
        private AuthConnector _authConnector;
        /*
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        */
        public AccountController()
        //public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _authConnector = new AuthConnector();
//            _userManager = userManager;
            //_signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new UserLoginData() { ReturnUrl = returnUrl });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginData authData)
        {
            HttpResponseMessage response = _authConnector.Login(authData);
            
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
    }
}
