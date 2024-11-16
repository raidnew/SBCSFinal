using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebClient.Controllers
{
    public class AuthController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            var jwt = HttpContext.Session.GetString("token");
            if (jwt != null) {
                var token = new JwtSecurityToken(jwt);
                var identity = new ClaimsIdentity(token.Claims, "basic");
                HttpContext.User = new ClaimsPrincipal(identity);
            }
            base.OnActionExecuting(ctx);
        }
    }
}
