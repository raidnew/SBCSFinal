using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("Data")]
        [AllowAnonymous]
        public ActionResult<string> TestAction()
        {
            string authHeader = HttpContext.Request.Headers[HeaderNames.Authorization];

            if (authHeader != null)
            {

                var jwtEncodedString = authHeader.Substring(7);

                var token = new JwtSecurityToken(jwtEncodedString: authHeader);

                var identity = new ClaimsIdentity(token.Claims, "basic");
                HttpContext.User = new ClaimsPrincipal(identity);
            }

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
            
            return test.ToString();
        }
    }
}
