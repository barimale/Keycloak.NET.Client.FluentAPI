using Keycloak.Net;
using System.Threading.Tasks;

namespace Keycloak.NET.FluentAPI.Manage.Events
{
    internal class LoginEvents : ILoginEvents
    {
        private readonly IRealmContext _context;
        private readonly KeycloakClient _client;

        public LoginEvents(IRealmContext context)
        {
            _context = context;
            _client = context.Client;
        }

        public async Task<bool> ResetAsync()
        {
            return await _client.DeleteEventsAsync(_context.ConnectionSettings.Realm);
        }
    }
}
