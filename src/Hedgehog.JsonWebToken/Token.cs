using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Hedgehog.JsonWebToken
{
    public class Token : IToken
    {
        public static SymmetricSecurityKey GetSymmetricSecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        }

        public static SigningCredentials GetSigningCredentials(SymmetricSecurityKey symmetricSecurityKey)
        {
            return new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        }

        public string CreateJsonWebToken(Claim[] claims, JwtSettings jwtSettings, JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                id = claims.Single(p => p.Type.Equals("id")).Value,
                auth_token = GenerateEncodedToken(claims, jwtSettings),
                expires_in = JwtOptions.GetValidFor(jwtSettings.Timeout)
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public string GetId(string authorization)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            var token = jwtSecurityTokenHandler.ReadToken(RemoveBearer(authorization)) as JwtSecurityToken;
            return token?.Claims.First(claim => claim.Type == "id").Value;
        }

        private string GenerateEncodedToken(IEnumerable<Claim> claims, JwtSettings settings)
        {
            var jwt = new JwtSecurityToken(
                issuer: settings.Issuer,
                audience: settings.Audience,
                claims: claims,
                notBefore: JwtOptions.GetNotBefore(),
                expires: JwtOptions.GetExpiration(JwtOptions.GetValidFor(settings.Timeout)),
                signingCredentials: settings.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        private static string RemoveBearer(string jsonWebToken)
        {
            return Regex.Replace(jsonWebToken, "bearer ", string.Empty, RegexOptions.IgnoreCase);
        }
    }
}
