using Newtonsoft.Json;

namespace Hedgehog.JsonWebToken
{
    public class TokenData
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("auth_token")]
        public string AuthToken { get; set; }

        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }
    }
}
