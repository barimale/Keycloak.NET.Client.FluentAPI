using Flurl;
using Flurl.Http;
using Keycloak.Net.Models.Roles;
using Keycloak.NET.Client.FluentAPI.Model;
using Keycloak.NET.FluentAPI.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Keycloak.NET.FluentAPI
{
    public class AuthorizationManager : IManager
    {
        public Net.Models.Users.User User { get; private set; }

        private readonly List<string> Priviligies = new List<string>();

        private readonly List<string> RealmPriviligies = new List<string>();


        public AccessTokenResponse Token { get; private set; }


        public Task<bool> AuthorizeAsync(IContext context, CancellationToken token = default)
        {
            try
            {
                switch(context.ProtocolType)
                {
                    case ClientProtocolType.openIdConnect:
                        return UsingOpenIdConnectAsync(context, token);
                    case ClientProtocolType.saml:
                        return UsingSamlAsync(context, token);
                    default:
                        throw new ArgumentException("Argument value not supported.", "ProtocolType");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Task<bool> UsingOpenIdConnectAsync(IContext context, CancellationToken token)
        {
            switch (context.ProtocolAccessType)
            {
                case AccessType.Confidential:
                    return InConfidentialWayAsync(context, token);
                case AccessType.Public:
                    return InPublicWayAsync(context, token);
                case AccessType.Bearer_only:
                    return InServiceWayAsync(context, token);
                default:
                    throw new ArgumentException("Argument value not supported.", "ProtocolAccessType");
            }
        }

        private async Task<bool> UsingSamlAsync(IContext context, CancellationToken token = default)
        {
            throw new NotSupportedException("SAML protocol not supported for Desktop client.");
        }

        private async Task<bool> InServiceWayAsync(IContext context, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        private async Task<bool> InConfidentialWayAsync(IContext context, CancellationToken token = default)
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
            }
            catch (Exception ex)
            {
                var i = 0;
                throw;
            }

            return GetClaims(context);
        }

        private async Task<bool> InPublicWayAsync(IContext context, CancellationToken token = default)
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

        private bool GetClaims(IContext context)
        {
            try
            {
                var stream = Token.token.ToString();
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(stream);
                var tokenS = handler.ReadToken(Token.token) as JwtSecurityToken;

                Token.otherClaims = tokenS.Payload;

                var userId = tokenS.Claims.First(p => p.Type == "sub").Value;
                User = new Net.Models.Users.User()
                {
                    Id = userId
                };

                var realmAccess = tokenS.Claims.First(p => p.Type == "realm_access").Value;
                var realmRoles = JsonConvert.DeserializeObject<RealmRoles>(realmAccess);

                var resourceAccess = tokenS.Claims.First(p => p.Type == "resource_access").Value;
                var resourceAccessDes = JsonConvert.DeserializeObject<Dictionary<string, RealmRoles>>(resourceAccess);
                var otherRoles = resourceAccessDes.FirstOrDefault(p => p.Key == context.ConnectionSettings.ClientName).Value;

                if(realmRoles!=null)
                    RealmPriviligies.AddRange(realmRoles.Names);

                if (otherRoles != null)
                    Priviligies.AddRange(otherRoles.Names);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ImmutableList<string> RealmPriviligiesAsListOfNames()
        {
            return RealmPriviligies.ToImmutableList();
        }

        public ImmutableList<string> PriviligiesAsListOfNames()
        {
            return Priviligies.ToImmutableList();
        }

        public ImmutableList<Role> RealmPriviligiesAsListOfRoles()
        {
            return RealmPriviligies.Select(p =>
            {
                return new Role
                {
                    Name = p
                };
            }).ToImmutableList();
        }

        public ImmutableList<Role> PriviligiesAsListOfRoles()
        {
           return Priviligies.Select(p =>
            {
                return new Role
                {
                    Name = p
                };
            }).ToImmutableList();
        }

        public ImmutableList<AtributedRole> AttributedPriviligiesAsListOfRoles()
        {
            throw new NotImplementedException();
        }
    }
}