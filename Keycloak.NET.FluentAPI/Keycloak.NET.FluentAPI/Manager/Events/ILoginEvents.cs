using System.Threading.Tasks;

namespace Keycloak.NET.FluentAPI.Manage.Events
{
    public interface ILoginEvents
    {
        Task<bool> ResetAsync();
    }
}