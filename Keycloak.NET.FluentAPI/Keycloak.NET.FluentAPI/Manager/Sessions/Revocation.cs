using Keycloak.Net;
using System;
using System.Threading.Tasks;

namespace Keycloak.NET.FluentAPI.Manage.Sessions
{
    internal class Revocation : IRevocation
    {
        private readonly IRealmContext _context;
        private readonly KeycloakClient _client;

        public Revocation(IRealmContext context)
        {
            _context = context;
            _client = context.Client;
        }

        public async Task<bool> SetToNowAndPushAsync()
        {
            throw new NotImplementedException();
        }
    }
}