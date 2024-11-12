using Microsoft.IdentityModel.Tokens;

namespace API.Auth
{
    public interface IJwtSigningDecodingKey
    {
        SecurityKey GetKey();
    }
}
