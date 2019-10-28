using Keycloak.Net.Models.RealmsAdmin;
using System.Threading.Tasks;

namespace Keycloak.NET.FluentAPI.Manage.Import
{
    public interface IPartialImport
    {
        Task<bool> ImportAsync(Realm input);
        Task<bool> ImportAsync(byte[] file);
    }
}