using Flurl;
using Flurl.Http;
using Keycloak.NET.FluentAPI.Model;
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

        public List<string> Priviligies { get; private set; } = new List<string>();

        public AccessTokenResponse Token { get; private set; }


        public async Task<bool> Authorize(IContext context, CancellationToken token = default)
        {
            try
            {
                switch(context.ProtocolType)
                {
                    case IContext.ClientProtocolType.openIdConnect:
                        return await UsingOpenIdConnect(context, token);
                    case IContext.ClientProtocolType.saml:
                        return await UsingSaml(context, token);
                    default:
                        throw new ArgumentException("Argument value not supported.", "ProtocolType");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<bool> UsingOpenIdConnect(IContext context, CancellationToken token)
        {
            switch (context.ProtocolAccessType)
            {
                case IContext.AccessType.Confidential:
                    return await InConfidentialWay(context, token);
                case IContext.AccessType.Public:
                    return await InPublicWay(context, token);
                case IContext.AccessType.Bearer_only:
                    return await InServiceWay(context, token);
                default:
                    throw new ArgumentException("Argument value not supported.", "ProtocolAccessType");
            }
        }

        private async Task<bool> UsingSaml(IContext context, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        private async Task<bool> InServiceWay(IContext context, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        private async Task<bool> InConfidentialWay(IContext context, CancellationToken token = default)
        {
            try
            {
                Token = await context.ConnectionSettings.Url
                    .AppendPathSegment($"/auth/realms/{context.ConnectionSettings.Realm}/protocol/openid-connect/token")
                    .WithHeader("Content-Type", "application/x-www-form-urlencoded")
                    .WithBasicAuth(context.ConnectionSettings.ClientName, context.ConnectionSettings.ClientSecret)
                    .PostUrlEncodedAsync(new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("username", context.ConnectionSettings.Username),
                        new KeyValuePair<string, string>("password", context.ConnectionSettings.Password),
                        new KeyValuePair<string, string>("client_id", context.ConnectionSettings.ClientName)
                    }, cancellationToken: token)
                    .ReceiveJson<AccessTokenResponse>();

                return GetClaims(context);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<bool> InPublicWay(IContext context, CancellationToken token = default)
        {

            try
            {
                Token = await context.ConnectionSettings.Url
                    .AppendPathSegment($"/auth/realms/{context.ConnectionSettings.Realm}/protocol/openid-connect/token")
                    .WithHeader("Content-Type", "application/x-www-form-urlencoded")
                    .PostUrlEncodedAsync(new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("username", context.ConnectionSettings.Username),
                        new KeyValuePair<string, string>("password", context.ConnectionSettings.Password),
                        new KeyValuePair<string, string>("client_id", context.ConnectionSettings.ClientName)
                    }, cancellationToken: token)
                    .ReceiveJson<AccessTokenResponse>();

                return GetClaims(context);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool GetClaims(IContext context)
        {
            try
            {
                var stream = Token.token.ToString();
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(stream);
                var tokenS = handler.ReadToken(Token.token) as JwtSecurityToken;

                Token.otherClaims = tokenS.Payload;

                UserId = tokenS.Claims.First(p => p.Type == "sub").Value;

                var realmAccess = tokenS.Claims.First(p => p.Type == "realm_access").Value;
                var realmRoles = JsonConvert.DeserializeObject<RealmRoles>(realmAccess);

                var resourceAccess = tokenS.Claims.First(p => p.Type == "resource_access").Value;
                var resourceAccessDes = JsonConvert.DeserializeObject<Dictionary<string, RealmRoles>>(resourceAccess);
                var otherRoles = resourceAccessDes.FirstOrDefault(p => p.Key == context.ConnectionSettings.ClientName).Value;

                if(realmRoles!=null)
                    Priviligies.AddRange(realmRoles.Names);

                if (otherRoles != null)
                    Priviligies.AddRange(otherRoles.Names);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}