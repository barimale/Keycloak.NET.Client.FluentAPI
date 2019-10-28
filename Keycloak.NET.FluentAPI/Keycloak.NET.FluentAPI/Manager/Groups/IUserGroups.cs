using System.Threading.Tasks;

namespace Keycloak.NET.FluentAPI.Manage
{
    public interface IUserGroups
    {
        Task<bool> Create(string name);
        Task<bool> Delete(string groupId);
        Task<bool> Rename(string groupId, string newName);
    }
}