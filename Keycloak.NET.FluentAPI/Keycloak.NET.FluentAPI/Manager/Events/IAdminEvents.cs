using System.Threading.Tasks;

namespace Keycloak.NET.FluentAPI.Manage.Events
{
    public interface IAdminEvents
    {
        Task<bool> ResetAsync();
    }
}