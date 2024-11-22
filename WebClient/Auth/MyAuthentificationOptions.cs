using Microsoft.AspNetCore.Authentication;

namespace WebClient.Auth
{
    public class MyAuthenticationOptions :
        AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "MyAuthenticationScheme";
        public string TokenHeaderName { get; set; } = "MyToken";
    }
}
