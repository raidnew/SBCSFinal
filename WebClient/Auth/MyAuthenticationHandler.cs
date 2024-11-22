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

            /*
            if (!Request.Headers
                .ContainsKey(Options.TokenHeaderName))
            {
                return AuthenticateResult.Fail($"Missing header: {Options.TokenHeaderName}");
            }

            //get the header and validate
            string token = Request
                .Headers[Options.TokenHeaderName]!;

            //usually, this is where you decrypt a token and/or lookup a database.
            if (token != "supersecretecode")
            {
                return AuthenticateResult
                    .Fail($"Invalid token.");
            }
            //Success! Add details here that identifies the user
            var claims = new List<Claim>()
        {
            new Claim("FirstName", "Juan")
        };

            var claimsIdentity = new ClaimsIdentity
                (claims, this.Scheme.Name);
            var claimsPrincipal = new ClaimsPrincipal
                (claimsIdentity);

            return AuthenticateResult.Success
                (new AuthenticationTicket(claimsPrincipal,
                this.Scheme.Name));
            */
        }
    }
}
