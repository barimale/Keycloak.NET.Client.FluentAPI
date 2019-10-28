using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keycloak.NET.FluentAPI.Configure
{
    internal class ClientScope : IClientScopes
    {
        private readonly IRealmContext _context;

        public ClientScope(IRealmContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(IEnumerable<Net.Models.Roles.Role> roles, string clientScopeName)
        {
            try
            {
                var userId = _context.UserDetails.Id;
                var _client = _context.Client;

                var clientScopes = await _client.GetClientScopesAsync(_context.ConnectionSettings.Realm);
                var clientScopeId = clientScopes.FirstOrDefault(p => p.Name == clientScopeName).Id;

                var clients = await _client.GetClientsAsync(_context.ConnectionSettings.Realm);
                var clientId = clients.FirstOrDefault(p => p.ClientId == _context.ConnectionSettings.ClientName).Id;

                return await _client.AddClientRolesToClientScopeAsync(
                    _context.ConnectionSettings.Realm,
                    clientScopeId,
                    clientId,
                    roles);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }
    }
}
