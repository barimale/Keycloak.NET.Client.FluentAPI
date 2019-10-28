using Keycloak.Net;
using Keycloak.Net.Models.RealmsAdmin;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace Keycloak.NET.FluentAPI.Manage.Export
{
    internal class PartialExport : IPartialExport
    {
        private readonly IRealmContext _context;
        private readonly KeycloakClient _client;

        public PartialExport(IRealmContext context)
        {
            _context = context;
            _client = context.Client;
        }

        public async Task<Realm> ExportAsync(bool withGroupsAndRoles = false, bool withClients = false)
        {
            return await _client.RealmPartialExportAsync(
                _context.ConnectionSettings.Realm, 
                withGroupsAndRoles, 
                withClients);
        }

        public async Task<byte[]> ExportToByteArrayAsync(bool withGroupsAndRoles = false, bool withClients = false)
        {
            var result = await ExportAsync(withGroupsAndRoles, withClients);
            string output = JsonConvert.SerializeObject(result);

            return Encoding.ASCII.GetBytes(output);
        }
    }
}
