using Keycloak.NET.FluentAPI.Model;
using Keycloak.NET.FluentAPI.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;

namespace Keycloak.NET.FluentAPI
{
    public class AuthenticatorManager
    {
        public List<string> Entitlements = new List<string>();

        private AccessTokenResponse tokenResponse;

        //TODO: extend builder -> choosing access type : public, confidential, bearer-only
        //TODO: replace Webclient by Flurl
        public bool AuthenticateConfidential(IConnectionSettings settings)
        {
            string endpoint = settings.Url + "auth/realms/" + settings.Realm + "/protocol/openid-connect/token";
            string method = "POST";

            using (WebClient wc = new WebClient())
            {
                string credentials = Convert.ToBase64String(
                    Encoding.ASCII.GetBytes(settings.ClientName + ":" + settings.ClientSecret));
                wc.Headers[HttpRequestHeader.Authorization] = string.Format(
                    "Basic {0}", credentials);

                wc.Headers["Content-Type"] = "application/x-www-form-urlencoded";

                try
                {
                    var data = "username=" + settings.Username + "&password=" + settings.Password + "&client_id=" + settings.ClientName + "&grant_type=password";

                    string response = wc.UploadString(endpoint, method, data);

                    tokenResponse = JsonConvert.DeserializeObject<AccessTokenResponse>(response);

                    return GetClaims(settings);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool AuthenticatePublic(IConnectionSettings settings)
        {
            string endpoint = settings.Url + "auth/realms/" + settings.Realm  + "/protocol/openid-connect/token";
            string method = "POST";

            using (WebClient wc = new WebClient())
            {
                wc.Headers["Content-Type"] = "application/x-www-form-urlencoded";
                try
                {
                    var data = "username=" + settings.Username + "&password="+ settings.Password + "&client_id="+ settings.ClientName +"&grant_type=password";
                    string response = wc.UploadString(endpoint, method, data);

                    tokenResponse = JsonConvert.DeserializeObject<AccessTokenResponse>(response);

                    return GetClaims(settings);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private bool GetClaims(IConnectionSettings settings)
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
                var otherRoles = resourceAccessDes.FirstOrDefault(p => p.Key == settings.ClientName).Value;

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
