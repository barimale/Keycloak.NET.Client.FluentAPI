using System.Threading.Tasks;

namespace Keycloak.NET.FluentAPI.Manage.Groups
{
    public interface IDefaultGroups
    {
        Task<bool> AddAsync(string name);
        Task<bool> RemoveAsync(string groupId);
    }
}