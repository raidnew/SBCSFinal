using API.Auth;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(
                authRequest.Name,
                authRequest.Password,
                false,
                false);

            List<Claim> claimsList;

            if (result.Succeeded)
            {
                AppUser user = await _userManager.FindByNameAsync(authRequest.Name);
                claimsList = (List<Claim>)_userManager.GetClaimsAsync(user).Result;
                
                claimsList.Add(new Claim(ClaimTypes.Name, authRequest.Name));
                claimsList.Add(new Claim(ClaimTypes.Authentication, authRequest.Name));
                claimsList.Add(new Claim(ClaimTypes.NameIdentifier, authRequest.Name));
                claimsList.Add(new Claim(ClaimTypes.Role, authRequest.Name));
            }
            else
            {
                claimsList = new List<Claim>();
                claimsList.Add(new Claim(ClaimTypes.Name, authRequest.Name));
            }

            Claim[] claims = claimsList.ToArray();

            var token = new JwtSecurityToken(
                issuer: "testi",
                audience: "testa",
                claims: claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: new SigningCredentials(
                        signingEncodingKey.GetKey(),
                        signingEncodingKey.SigningAlgorithm)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "test";
        }
    }
}
