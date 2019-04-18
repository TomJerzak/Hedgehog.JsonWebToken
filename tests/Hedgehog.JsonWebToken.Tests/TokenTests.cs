using FluentAssertions;
using Xunit;
using Newtonsoft.Json;

namespace Hedgehog.JsonWebToken.Tests
{
    public class TokenTests
    {
        [Fact]
        public void return_json_web_token()
        {
            IToken token = new Token();
            IClaims claims = new Claims();
            claims.Create("1", "User");

            var jwt = token.CreateJsonWebToken(claims.ToArray(), new JwtSettings() {Timeout = 120}, new JsonSerializerSettings {Formatting = Formatting.Indented});

            jwt.Should().Contain("\"id\": \"1\",");
            jwt.Should().Contain("\"auth_token\": \"");
            jwt.Should().Contain("\"expires_in\": \"02:00:00\"");
        }
    }
}
