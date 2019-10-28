using Keycloak.Net.Models.RealmsAdmin;
using System.Threading.Tasks;

namespace Keycloak.NET.FluentAPI.Manage.Export
{
    public interface IPartialExport
    {
        Task<Realm> ExportAsync(bool withGroupsAndRoles = false, bool withClients = false);
        Task<byte[]> ExportToByteArrayAsync(bool withGroupsAndRoles = false, bool withClients = false);
    }
}