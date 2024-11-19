using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
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
using Microsoft.AspNetCore.Identity;

namespace WebClient.Controllers
{
    public class AccountController : BaseMyController
    {
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToMainPage();
            else
                return View(new UserLoginData() { ReturnUrl = returnUrl });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginData authData)
        {
            if (ModelState.IsValid)
            {
                AuthenticationRequest request = new AuthenticationRequest();
                request.Name = authData.UserName;
                request.Password = authData.Password;

                ApiConnector apiConnector = new ApiConnector(HttpContext);
                string jwt = await apiConnector.AuthRequest(request);
                AuthUserByJwt(jwt);
            }
            return View(authData);
        }

        public IActionResult LoginInfo()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("token");
            HttpContext.User = new ClaimsPrincipal();
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
            return RedirectToAction("Index", "Home");
        }
    }
}
