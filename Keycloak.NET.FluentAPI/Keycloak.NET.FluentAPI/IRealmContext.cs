using Keycloak.Net.Models.Users;
using Keycloak.NET.FluentAPI.Base;
using Keycloak.NET.FluentAPI.Configure;
using Keycloak.NET.FluentAPI.Manage;

namespace Keycloak.NET.FluentAPI
{
    public interface IRealmContext : IBaseContext
    {
        IConfigurator Configurator { get; }
        IManager Manager { get; }
        User UserDetails { get; }
    }
}