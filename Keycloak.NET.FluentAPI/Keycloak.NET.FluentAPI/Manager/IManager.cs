using Keycloak.NET.FluentAPI.Manage.Events;
using Keycloak.NET.FluentAPI.Manage.Export;
using Keycloak.NET.FluentAPI.Manage.Import;
using Keycloak.NET.FluentAPI.Manage.Sessions;

namespace Keycloak.NET.FluentAPI.Manage
{
    public interface IManager
    {
        IGroup Groups { get; }
        IEvents Events { get; }
        ISession Sessions { get; }
        IPartialImport Import { get; }
        IPartialExport Export { get; }
    }
}