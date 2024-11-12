using API.Auth;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [AllowAnonymous]
        public ActionResult<string> Post(AuthenticationRequest authRequest, [FromServices] IJwtSigningEncodingKey signingEncodingKey)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, authRequest.Name)
            };

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
        public ActionResult<IEnumerable<string>> Get()
        {
            var nameIdentifier = this.HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            return new string[] { nameIdentifier?.Value, "value1", "value2" };
        }
    }
}
