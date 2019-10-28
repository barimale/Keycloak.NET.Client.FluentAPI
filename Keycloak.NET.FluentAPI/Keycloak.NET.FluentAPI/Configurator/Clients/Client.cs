using Keycloak.Net;
using Keycloak.Net.Models.Clients;
using System;
using System.Threading.Tasks;

namespace Keycloak.NET.FluentAPI.Configure
{
    internal class Client : IClients
    {
        private readonly IRealmContext _context;
        private readonly KeycloakClient _client;

        public Client(IRealmContext context)
        {
            _context = context;
            _client = context.Client;
        }

        public async Task<bool> Create(string clientId, Protocol protocolType, string endpoint = "")
        {
            try
            {
                var client = new Net.Models.Clients.Client
                {
                    ClientId = clientId,
                    Protocol = protocolType.ToString(),
                    BaseUrl = endpoint
                };

                return await _client.CreateClientAsync(_context.ConnectionSettings.Realm, client);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
