using System.Threading.Tasks;

namespace Keycloak.NET.FluentAPI.Manage.Sessions
{
    public interface IRevocation
    {
        Task<bool> SetToNowAndPushAsync();
    }
}