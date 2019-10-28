using Keycloak.Net;
using System.Threading.Tasks;

namespace Keycloak.NET.FluentAPI.Manage.Events
{
    internal class AdminEvents : IAdminEvents
    {
        private readonly IRealmContext _context;
        private readonly KeycloakClient _client;

        public AdminEvents(IRealmContext context)
        {
            _context = context;
            _client = context.Client;
        }

        public async Task<bool> ResetAsync()
        {
            return await _client.DeleteAdminEventsAsync(_context.ConnectionSettings.Realm);
        }
    }
}
