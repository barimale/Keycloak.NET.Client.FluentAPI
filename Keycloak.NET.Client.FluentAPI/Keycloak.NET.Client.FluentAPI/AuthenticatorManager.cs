using Flurl;
using Flurl.Http;
using Keycloak.NET.FluentAPI.Model;
using Keycloak.NET.FluentAPI.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Keycloak.NET.FluentAPI
{
    public class AuthenticatorManager : IAuthenticatorManager
    {
        //TODO: replace it by using Keycloak.Net.Models.Users
        public string UserId { get; private set; }

        public List<string> Entitlements { get; private set; } = new List<string>();

        public AccessTokenResponse Token { get; private set; }

        public async Task<bool> InConfidentialWay(IConnectionSettings settings, CancellationToken token = default)
        {
            try
            {
                Token = await settings.Url
                    .AppendPathSegment($"/auth/realms/{settings.Realm}/protocol/openid-connect/token")
                    .WithHeader("Content-Type", "application/x-www-form-urlencoded")
                    .WithBasicAuth(settings.ClientName, settings.ClientSecret)
                    .PostUrlEncodedAsync(new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("username", settings.Username),
                        new KeyValuePair<string, string>("password", settings.Password),
                        new KeyValuePair<string, string>("client_id", settings.ClientName)
                    }, cancellationToken: token)
                    .ReceiveJson<AccessTokenResponse>();

                return GetClaims(settings);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //TODO: expose just Login method and decide about strategy based on clientSecret field

        public async Task<bool> InPublicWay(IConnectionSettings settings, CancellationToken token = default)
        {

            try
            {
                Token = await settings.Url
                    .AppendPathSegment($"/auth/realms/{settings.Realm}/protocol/openid-connect/token")
                    .WithHeader("Content-Type", "application/x-www-form-urlencoded")
                    .PostUrlEncodedAsync(new List<KeyValuePair<string, string>>
                    {
                                        new KeyValuePair<string, string>("grant_type", "password"),
                                        new KeyValuePair<string, string>("username", settings.Username),
                                        new KeyValuePair<string, string>("password", settings.Password),
                                        new KeyValuePair<string, string>("client_id", settings.ClientName)
                    }, cancellationToken: token)
                    .ReceiveJson<AccessTokenResponse>();

                return GetClaims(settings);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool GetClaims(IConnectionSettings settings)
        {
            try
            {
                var stream = Token.token.ToString();
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(stream);
                var tokenS = handler.ReadToken(Token.token) as JwtSecurityToken;

                UserId = tokenS.Claims.First(p => p.Type == "sub").Value;

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
