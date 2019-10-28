using Keycloak.Net;
using Keycloak.NET.FluentAPI.Base;
using System.Threading.Tasks;

namespace Keycloak.NET.FluentAPI.Realms
{
    internal class Realm : IRealm
    {
        private readonly BaseContext _context;
        private readonly KeycloakClient _client;

        //TODO: Context has to be modify
        public Realm(BaseContext context)
        {
            _context = context;
            _client = _context.Client;
        }

        public async Task<bool> DeleteAsync(string name)
        {
            return await _client.DeleteRealmAsync(name);
        }

        public async Task<bool> AddAsync(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
