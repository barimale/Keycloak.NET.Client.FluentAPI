using System.Threading.Tasks;

namespace Keycloak.NET.FluentAPI.Manage.Sessions
{
    public interface IRealmSessions
    {
        Task<bool> LogoutAllAsync();
    }
}