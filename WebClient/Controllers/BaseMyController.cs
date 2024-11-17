using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebClient.Net;

namespace WebClient.Controllers
{
    public class BaseMyController : Controller
    {
        protected ApiConnector ApiConnector { get; private set; }

        public BaseMyController()
        {
            ApiConnector = new ApiConnector(HttpContext);
        }

        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            var jwt = HttpContext.Session.GetString("token");
            if (jwt != null) AuthUserByJwt(jwt);
            base.OnActionExecuting(ctx);
        }

        protected void AuthUserByJwt(string jwt)
        {
            var token = new JwtSecurityToken(jwt);
            var identity = new ClaimsIdentity(token.Claims, "Identity.Application");
            HttpContext.User = new ClaimsPrincipal(identity);
        }
    }
}
