using Keycloak.Net.Models.Roles;
using Keycloak.NET.FluentAPI.Model;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

namespace Keycloak.NET.FluentAPI
{
    public interface IManager
    {
        Net.Models.Users.User User { get; }
        ImmutableList<string> PriviligiesAsListOfNames();
        ImmutableList<Role> PriviligiesAsListOfRoles();
        AccessTokenResponse Token { get; }
        Task<bool> Authorize(IContext context, CancellationToken token = default);
    }
}