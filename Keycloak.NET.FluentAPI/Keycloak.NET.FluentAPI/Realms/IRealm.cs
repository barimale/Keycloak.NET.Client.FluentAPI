using System.Threading.Tasks;

namespace Keycloak.NET.FluentAPI.Realms
{
    public interface IRealm
    {
        Task<bool> AddAsync(string name);
        Task<bool> DeleteAsync(string name);
    }
}