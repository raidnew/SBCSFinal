using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WebClient.Auth;
using WebClient.Models;
using WebClient.Net;

namespace WebClient.Controllers
{
    public class AccountController : AuthController
    {
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new UserLoginData() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginData authData)
        {

            AuthenticationRequest request = new AuthenticationRequest();
            request.Name = authData.UserName;
            request.Password = authData.Password;

            ApiConnector apiConnector = new ApiConnector();
            string jwt = await apiConnector.AuthRequest(request);
            HttpContext.Session.SetString("token", jwt);

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(jwt);
            var identity = new ClaimsIdentity(jwtSecurityToken.Claims, "basic");
            HttpContext.User = new ClaimsPrincipal(identity);

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
