using API.Auth;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public async Task<ActionResult<string>> PostAsync(AuthenticationRequest authRequest, [FromServices] IJwtSigningEncodingKey signingEncodingKey)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, authRequest.Name)
            };

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(
                authRequest.Name,
                authRequest.Password,
                false,
                false);


            if (result.Succeeded)
            {
                claims[0] = new Claim(ClaimTypes.Authentication, authRequest.Name);
            }
            else
            {
                claims[0] = new Claim(ClaimTypes.Anonymous, authRequest.Name);
            }

            var token = new JwtSecurityToken(
                issuer: "DemoApp",
                audience: "DemoAppClient",
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: new SigningCredentials(
                        signingEncodingKey.GetKey(),
                        signingEncodingKey.SigningAlgorithm)
            );

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            string test = "";

            foreach (Claim claim in this.HttpContext.User.Claims)
            {
                test += claim.Type + ":" + claim.Value + "\n";
            }


            foreach (ClaimsIdentity claim in this.HttpContext.User.Identities)
            {
                test += claim.Name + ":" + claim.ToString() + "\n";
            }

            
            test += this.HttpContext.User.Identity.Name + ":" + this.HttpContext.User.Identity.IsAuthenticated + "\n";

            return test;
        }
    }
}
