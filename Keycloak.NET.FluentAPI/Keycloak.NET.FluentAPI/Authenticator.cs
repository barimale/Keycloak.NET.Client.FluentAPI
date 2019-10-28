using Keycloak.NET.FluentAPI.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;

namespace Keycloak.NET.FluentAPI
{
    public class Authenticator
    {
        public List<string> Entitlements = new List<string>();

        private readonly string Username;
        private readonly string Password;
        private readonly string Url;
        private readonly string Realm;
        private readonly string ClientId;
        private readonly string ClientSecret;
        private AccessTokenResponse tokenResponse;

        public Authenticator(string username, string password, string url, string realm, string clientId, string clientSecret)
        {
            Username = username;
            Password = password;
            Url = url;
            Realm = realm;
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        public bool AuthenticateConfidential()
        {
            string endpoint = Url + "auth/realms/" + Realm + "/protocol/openid-connect/token";
            string method = "POST";

            using (WebClient wc = new WebClient())
            {
                string credentials = Convert.ToBase64String(
                    Encoding.ASCII.GetBytes(ClientId + ":" + ClientSecret));
                wc.Headers[HttpRequestHeader.Authorization] = string.Format(
                    "Basic {0}", credentials);

                wc.Headers["Content-Type"] = "application/x-www-form-urlencoded";

                try
                {
                    var data = "username=" + Username + "&password=" + Password + "&client_id=" + ClientId + "&grant_type=password";

                    string response = wc.UploadString(endpoint, method, data);

                    tokenResponse = JsonConvert.DeserializeObject<AccessTokenResponse>(response);

                    return GetClaims();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool AuthenticatePublic()
        {
            string endpoint = Url + "auth/realms/" + Realm  + "/protocol/openid-connect/token";
            string method = "POST";

            using (WebClient wc = new WebClient())
            {
                wc.Headers["Content-Type"] = "application/x-www-form-urlencoded";
                try
                {
                    var data = "username=" + Username + "&password="+ Password + "&client_id="+ ClientId +"&grant_type=password";
                    string response = wc.UploadString(endpoint, method, data);

                    tokenResponse = JsonConvert.DeserializeObject<AccessTokenResponse>(response);

                    return GetClaims();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private bool GetClaims()
        {
            try
            {
                var stream = tokenResponse.token.ToString();
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(stream);
                var tokenS = handler.ReadToken(tokenResponse.token) as JwtSecurityToken;

                var realmAccess = tokenS.Claims.First(p => p.Type == "realm_access").Value;
                var realmRoles = JsonConvert.DeserializeObject<RealmRoles>(realmAccess);

                var resourceAccess = tokenS.Claims.First(p => p.Type == "resource_access").Value;
                var resourceAccessDes = JsonConvert.DeserializeObject<Dictionary<string, RealmRoles>>(resourceAccess);
                var otherRoles = resourceAccessDes.FirstOrDefault(p => p.Key == ClientId).Value;

                Entitlements.AddRange(realmRoles.Names);
                Entitlements.AddRange(otherRoles.Names);
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }
    }
}
