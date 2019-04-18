using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Hedgehog.JsonWebToken
{
    public class Claims : IClaims
    {
        private List<Claim> _claims;

        public Claims()
        {
            _claims = new List<Claim>();
        }

        public Claims(List<Claim> claims)
        {
            _claims = claims;
        }

        public Claim GetClaim(string claimName)
        {
            return _claims.SingleOrDefault(c => c.Type == claimName);
        }

        public List<Claim> Create(string userId, string login)
        {
            return Create(userId, login, _claims);
        }

        public List<Claim> Create(string userId, string login, List<Claim> claims)
        {
            var jwtOptions = new JwtOptions();

            _claims = claims;
            _claims.Add(new Claim(JwtRegisteredClaimNames.Sub, login));
            _claims.Add(new Claim(JwtRegisteredClaimNames.Jti, jwtOptions.Jti));
            _claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(JwtOptions.GetIssuedAt()).ToString(), ClaimValueTypes.Integer64));
            _claims.Add(new Claim(Constants.JwtClaimIdentifiers.Id, userId));
            _claims.Add(new Claim(Constants.JwtClaimIdentifiers.Rol, Constants.JwtClaims.ApiAccess));

            return _claims;
        }

        public Claim[] ToArray()
        {
            var array = new Claim[_claims.Count];

            for (var i = 0; i < _claims.Count; i++)
                array[i] = _claims[i];

            return array;
        }

        private static long ToUnixEpochDate(DateTime date)
        {
            return (long) Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        }
    }
}
