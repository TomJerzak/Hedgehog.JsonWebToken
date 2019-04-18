using Microsoft.IdentityModel.Tokens;

namespace Hedgehog.JsonWebToken
{
    public class JwtSettings
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int Timeout { get; set; }

        public SigningCredentials SigningCredentials { get; set; }
    }
}
