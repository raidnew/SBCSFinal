using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebClient.Controllers
{
    public class BaseMyController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            var jwt = HttpContext.Session.GetString("token");
            if (jwt != null) AuthUserByJwt(jwt);
            base.OnActionExecuting(ctx);
        }

        protected void AuthUserByJwt(string jwt)
        {
            var token = new JwtSecurityToken(jwt);
            var identity = new ClaimsIdentity(token.Claims, "basic");
            HttpContext.User = new ClaimsPrincipal(identity);
        }
    }
}
