using Keycloak.NET.FluentAPI.Settings;

namespace Keycloak.NET.FluentAPI
{
    public interface IContext
    {
        IConnectionSettings ConnectionSettings { get; }
    }
}