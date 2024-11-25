using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace WebClient.Auth
{
    public class MyAuthenticationHandler :
    AuthenticationHandler<MyAuthenticationOptions>
    {
        public MyAuthenticationHandler
     (IOptionsMonitor<MyAuthenticationOptions> options,
     ILoggerFactory logger, UrlEncoder encoder,
     ISystemClock clock)
     : base(options, logger, encoder, clock)
        { }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //check header first

            string jwt = Context.Session.GetString("token");
            if(jwt == null)
            {
                return AuthenticateResult.Fail($"Not logged");
            }

            var token = new JwtSecurityToken(jwt);
            var identity = new ClaimsIdentity(token.Claims, this.Scheme.Name);
            var claimsPrincipal = new ClaimsPrincipal(identity);

            return AuthenticateResult.Success(
                new AuthenticationTicket(
                    claimsPrincipal,
                    this.Scheme.Name));

        }
    }
}
