using Keycloak.NET.FluentAPI.Manage.Groups;

namespace Keycloak.NET.FluentAPI.Manage
{
    public interface IGroup
    {
        IUserGroups UserGroups { get; }
        IDefaultGroups DefaultGroups { get; }
    }
}