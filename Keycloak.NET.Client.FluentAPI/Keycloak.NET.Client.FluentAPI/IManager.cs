using Keycloak.Net.Models.Roles;
using Keycloak.NET.Client.FluentAPI.Model;
using Keycloak.NET.FluentAPI.Model;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

namespace Keycloak.NET.FluentAPI
{
    public interface IManager
    {
        Net.Models.Users.User User { get; }
        ImmutableList<string> RealmPriviligiesAsListOfNames();
        ImmutableList<Role> RealmPriviligiesAsListOfRoles();
        ImmutableList<string> PriviligiesAsListOfNames();
        ImmutableList<Role> PriviligiesAsListOfRoles();
        ImmutableList<AtributedRole> AttributedPriviligiesAsListOfRoles();
        AccessTokenResponse Token { get; }
        Task<bool> AuthorizeAsync(IContext context, CancellationToken token = default);
    }
}