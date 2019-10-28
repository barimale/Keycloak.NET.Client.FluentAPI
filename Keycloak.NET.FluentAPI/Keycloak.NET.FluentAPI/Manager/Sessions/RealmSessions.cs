using Keycloak.Net;
using System;
using System.Threading.Tasks;

namespace Keycloak.NET.FluentAPI.Manage.Sessions
{
    internal class RealmSessions : IRealmSessions
    {
        private readonly IRealmContext _context;
        private readonly KeycloakClient _client;

        public RealmSessions(IRealmContext context)
        {
            _context = context;
            _client = context.Client;
        }

        //TODO: it has to be implemented at the keycloak.net level
        public async Task<bool> LogoutAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
