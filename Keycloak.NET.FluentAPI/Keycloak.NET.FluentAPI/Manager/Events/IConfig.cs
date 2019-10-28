using System.Threading.Tasks;
using Keycloak.Net.Models.RealmsAdmin;

namespace Keycloak.NET.FluentAPI.Manage.Events
{
    public interface IConfig
    {
        Task<RealmEventsConfig> GetAsync();
        Task<bool> UpdateAsync(RealmEventsConfig config);
    }
}