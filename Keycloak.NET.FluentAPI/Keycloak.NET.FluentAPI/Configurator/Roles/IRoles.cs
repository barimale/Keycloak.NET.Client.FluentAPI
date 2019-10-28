using System.Collections.Generic;
using System.Threading.Tasks;
using Keycloak.Net.Models.Roles;

namespace Keycloak.NET.FluentAPI.Configure
{
    public interface IRoles
    {
        Task<List<Net.Models.Roles.Role>> AllAsync();
    }
}