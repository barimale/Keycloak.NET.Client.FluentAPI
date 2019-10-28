using Keycloak.Net;
using Keycloak.Net.Models.RealmsAdmin;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace Keycloak.NET.FluentAPI.Manage.Import
{
    internal class PartialImport : IPartialImport
    {
        private readonly IRealmContext _context;
        private readonly KeycloakClient _client;

        public PartialImport(IRealmContext context)
        {
            _context = context;
            _client = context.Client;
        }

        public async Task<bool> ImportAsync(Realm input)
        {
            return await _client.ImportRealmAsync(_context.ConnectionSettings.Realm, input);
        }

        public async Task<bool> ImportAsync(byte[] file)
        {
            var input = Encoding.ASCII.GetString(file);
            var realm = JsonConvert.DeserializeObject<Realm>(input);
            
            return await ImportAsync(realm);
        }
    }
}
