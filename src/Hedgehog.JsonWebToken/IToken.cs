using System.Security.Claims;
using Newtonsoft.Json;

namespace Hedgehog.JsonWebToken
{
    public interface IToken
    {
        string CreateJsonWebToken(Claim[] claims, JwtSettings jwtSettings, JsonSerializerSettings serializerSettings);
    }
}
