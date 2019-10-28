using Keycloak.Net;
using Keycloak.NET.FluentAPI.Settings;

namespace Keycloak.NET.FluentAPI.Base
{
    public interface IBaseContext
    {
        IConnectionSettings ConnectionSettings { get; }
        KeycloakClient Client { get; }
    }
}