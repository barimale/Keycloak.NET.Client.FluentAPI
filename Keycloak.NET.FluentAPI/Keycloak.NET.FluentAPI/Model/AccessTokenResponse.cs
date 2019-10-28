using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Keycloak.NET.FluentAPI.Model
{
    public class AccessTokenResponse
    {

        [JsonProperty("access_token")]
        public String token;

        [JsonProperty("expires_in")]
        public long expiresIn;

        [JsonProperty("refresh_expires_in")]
        public long refreshExpiresIn;

        [JsonProperty("refresh_token")]
        public String refreshToken;

        [JsonProperty("token_type")]
        public String tokenType;

        [JsonProperty("id_token")]
        public String idToken;

        [JsonProperty("not-before-policy")]
        public int notBeforePolicy;

        [JsonProperty("session_state")]
        public String sessionState;

        public Dictionary<String, Object> otherClaims = new Dictionary<String, Object>();

        //  OIDC Financial API Read Only Profile : scope MUST be returned in the response from Token Endpoint
        [JsonProperty("scope")]
        public String scope;
    }
}
