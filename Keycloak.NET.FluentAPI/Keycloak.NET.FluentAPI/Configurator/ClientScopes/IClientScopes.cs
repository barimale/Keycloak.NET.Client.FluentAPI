using System.Collections.Generic;
using System.Threading.Tasks;
using Keycloak.Net.Models.Roles;

namespace Keycloak.NET.FluentAPI.Configure
{
    public interface IClientScopes
    {
        Task<bool> Add(IEnumerable<Net.Models.Roles.Role> roles, string clientScopeName);
    }
}