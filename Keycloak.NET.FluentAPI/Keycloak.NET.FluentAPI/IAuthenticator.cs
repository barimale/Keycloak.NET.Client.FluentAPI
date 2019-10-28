using Keycloak.Net.Models.Users;

namespace Keycloak.NET.FluentAPI
{
    public interface IAuthenticator
    {
        User Authenticate();
    }
}