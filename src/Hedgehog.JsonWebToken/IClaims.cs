using System.Collections.Generic;
using System.Security.Claims;

namespace Hedgehog.JsonWebToken
{
    public interface IClaims
    {
        Claim GetClaim(string claimName);

        List<Claim> Create(string userId, string login);

        List<Claim> Create(string userId, string login, List<Claim> claims);

        Claim[] ToArray();
    }
}
