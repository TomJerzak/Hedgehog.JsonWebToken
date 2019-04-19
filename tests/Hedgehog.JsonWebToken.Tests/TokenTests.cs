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

            var jsonWebToken = token.CreateJsonWebToken(claims.ToArray(), new JwtSettings() {Timeout = 120}, new JsonSerializerSettings {Formatting = Formatting.Indented});

            jsonWebToken.Should().Contain("\"id\": \"1\",");
            jsonWebToken.Should().Contain("\"auth_token\": \"");
            jsonWebToken.Should().Contain("\"expires_in\": \"02:00:00\"");
        }

        [Fact]
        public void read_data_from_token()
        {
            IToken token = new Token();
            IClaims claims = new Claims();
            claims.Create("1", "User");
            var jsonWebToken = token.CreateJsonWebToken(claims.ToArray(), new JwtSettings() {Timeout = 120}, new JsonSerializerSettings {Formatting = Formatting.Indented});

            var id = token.GetId(JsonConvert.DeserializeObject<TokenData>(jsonWebToken).AuthToken);

            id.Should().Be("1");
        }

        [Fact]
        public void remove_bearer_and_read_data_from_token()
        {
            var jsonWebToken1 = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhZG1pbiIsImp0aSI6IjIzZDRhZjliLTBiYzUtNGYwMi1hMGFiLTY4OGZlNTMxM2EzNyIsImlhdCI6MTU1NTYyNTI4NSwiaWQiOiIyNzIxIiwicm9sIjoiYXBpX2FjY2VzcyIsIm5iZiI6MTU1NTYyNTI4NCwiZXhwIjoxNTU1NjMyNDg0LCJpc3MiOiJEb3RLaXQuV2ViLkV4YW1wbGUiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUwOTM2In0.jOF5fFcd5NBvwPlcaEzNCsVIVJg2uHx37xBECj3DIos";
            var jsonWebToken2 = "BEARER eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhZG1pbiIsImp0aSI6IjIzZDRhZjliLTBiYzUtNGYwMi1hMGFiLTY4OGZlNTMxM2EzNyIsImlhdCI6MTU1NTYyNTI4NSwiaWQiOiIyNzIxIiwicm9sIjoiYXBpX2FjY2VzcyIsIm5iZiI6MTU1NTYyNTI4NCwiZXhwIjoxNTU1NjMyNDg0LCJpc3MiOiJEb3RLaXQuV2ViLkV4YW1wbGUiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUwOTM2In0.jOF5fFcd5NBvwPlcaEzNCsVIVJg2uHx37xBECj3DIos";
            var jsonWebToken3 = "BeAReR eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhZG1pbiIsImp0aSI6IjIzZDRhZjliLTBiYzUtNGYwMi1hMGFiLTY4OGZlNTMxM2EzNyIsImlhdCI6MTU1NTYyNTI4NSwiaWQiOiIyNzIxIiwicm9sIjoiYXBpX2FjY2VzcyIsIm5iZiI6MTU1NTYyNTI4NCwiZXhwIjoxNTU1NjMyNDg0LCJpc3MiOiJEb3RLaXQuV2ViLkV4YW1wbGUiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUwOTM2In0.jOF5fFcd5NBvwPlcaEzNCsVIVJg2uHx37xBECj3DIos";
            IToken token = new Token();

            var id1 = token.GetId(jsonWebToken1);
            var id2 = token.GetId(jsonWebToken2);
            var id3 = token.GetId(jsonWebToken3);

            id1.Should().Be("2721");
            id2.Should().Be("2721");
            id3.Should().Be("2721");
        }
    }
}
