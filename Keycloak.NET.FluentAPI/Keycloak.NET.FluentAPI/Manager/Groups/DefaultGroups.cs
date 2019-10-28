using Keycloak.Net;
using System.Threading.Tasks;

namespace Keycloak.NET.FluentAPI.Manage.Groups
{
    internal class DefaultGroups : IDefaultGroups
    {
        private readonly IRealmContext _context;
        private readonly KeycloakClient _client;

        public DefaultGroups(IRealmContext context)
        {
            _context = context;
            _client = context.Client;
        }

        public async Task<bool> AddAsync(string name)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> RemoveAsync(string groupId)
        {
            throw new System.NotImplementedException();
        }
    }
}
