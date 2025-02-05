using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Keycloak.NET.FluentAPI.Model
{
    public class AccessTokenResponse
    {

        [JsonPropertyName("access_token")]
        public String token { get; set; }

        [JsonPropertyName("expires_in")]
        public long expiresIn { get; set; }

        [JsonPropertyName("refresh_expires_in")]
        public long refreshExpiresIn { get; set; }

        [JsonPropertyName("refresh_token")]
        public String refreshToken { get; set; }

        [JsonPropertyName("token_type")]
        public String tokenType { get; set; }

        [JsonPropertyName("id_token")]
        public String idToken { get; set; }

        [JsonPropertyName("not-before-policy")]
        public int notBeforePolicy { get; set; }

        [JsonPropertyName("session_state")]
        public String sessionState { get; set; }

        public Dictionary<String, Object> otherClaims = new Dictionary<String, Object>();

        [JsonPropertyName("scope")]
        public String scope { get; set; }
    }
}
