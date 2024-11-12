using Microsoft.IdentityModel.Tokens;

namespace API.Auth
{
    public interface IJwtSigningEncodingKey
    {
        string SigningAlgorithm { get; }

        SecurityKey GetKey();
    }
}
