using Keycloak.Net;
using Keycloak.Net.Models.RealmsAdmin;
using System.Threading.Tasks;

namespace Keycloak.NET.FluentAPI.Manage.Events
{
    internal class Config : IConfig
    {
        private readonly IRealmContext _context;
        private readonly KeycloakClient _client;

        public Config(IRealmContext context)
        {
            _context = context;
            _client = context.Client;
        }

        public async Task<RealmEventsConfig> GetAsync()
        {
            return await _client.GetRealmEventsProviderConfigurationAsync(_context.ConnectionSettings.Realm);
        }

        public async Task<bool> UpdateAsync(RealmEventsConfig config)
        {
            return await _client.UpdateRealmEventsProviderConfigurationAsync(_context.ConnectionSettings.Realm, config);
        }
    }
}
