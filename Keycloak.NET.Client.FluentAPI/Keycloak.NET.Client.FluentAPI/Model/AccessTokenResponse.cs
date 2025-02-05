using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Keycloak.NET.FluentAPI.Model
{
    public class AccessTokenResponse
    {

        [JsonPropertyName("access_token")]
        public String token;

        [JsonPropertyName("expires_in")]
        public long expiresIn;

        [JsonPropertyName("refresh_expires_in")]
        public long refreshExpiresIn;

        [JsonPropertyName("refresh_token")]
        public String refreshToken;

        [JsonPropertyName("token_type")]
        public String tokenType;

        [JsonPropertyName("id_token")]
        public String idToken;

        [JsonPropertyName("not-before-policy")]
        public int notBeforePolicy;

        [JsonPropertyName("session_state")]
        public String sessionState;

        public Dictionary<String, Object> otherClaims = new Dictionary<String, Object>();

        [JsonPropertyName("scope")]
        public String scope;
    }
}
