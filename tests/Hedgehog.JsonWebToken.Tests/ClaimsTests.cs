using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FluentAssertions;
using Xunit;

namespace Hedgehog.JsonWebToken.Tests
{
    public class ClaimsTests
    {
        [Fact]
        public void return_claim()
        {
            IClaims claims = new Claims();
            claims.Create("1", "Test");

            claims.GetClaim(JwtRegisteredClaimNames.Sub).Value.Should().Be("Test");
        }

        [Fact]
        public void return_claims()
        {
            IClaims claims = new Claims();

            var claims1 = claims.Create("1", "Test");
            var claims2 = claims.Create("1", "Test", new List<Claim>());

            claims1.Count.Should().BeGreaterThan(0);
            claims2.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void claim_is_null()
        {
            IClaims claims = new Claims();
            claims.Create("1", "Test");

            claims.GetClaim(JwtRegisteredClaimNames.Website).Should().BeNull();
        }

        [Fact]
        public void create_claims()
        {
            IClaims claims = new Claims(new List<Claim>() {new Claim("OwnClaim", "Test")});

            claims.Create("1", "Test");

            claims.GetClaim(JwtRegisteredClaimNames.Sub).Value.Should().Be("Test");
            claims.GetClaim(JwtRegisteredClaimNames.Jti).Value.Length.Should().Be(36);
            claims.GetClaim(JwtRegisteredClaimNames.Iat).Value.Should().NotBeNullOrEmpty();
            claims.GetClaim(Constants.JwtClaimIdentifiers.Id).Value.Should().Be("1");
            claims.GetClaim(Constants.JwtClaimIdentifiers.Rol).Value.Should().Be(Constants.JwtClaims.ApiAccess);
            claims.GetClaim("OwnClaim").Value.Should().Be("Test");
        }

        [Fact]
        public void convert_claims_to_array()
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, "Test"),
                new Claim(JwtRegisteredClaimNames.Jti, new JwtOptions().Jti)
            };

            var result = new Claims(claims).ToArray();

            result.Length.Should().Be(2);
            result[0].Value.Should().Be("Test");
        }
    }
}
