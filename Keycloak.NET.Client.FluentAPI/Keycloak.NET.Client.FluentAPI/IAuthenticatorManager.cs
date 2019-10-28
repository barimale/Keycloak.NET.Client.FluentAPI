using System.Collections.Generic;
using System.Threading.Tasks;
using Keycloak.NET.FluentAPI.Model;
using Keycloak.NET.FluentAPI.Settings;

namespace Keycloak.NET.FluentAPI
{
    public interface IAuthenticatorManager
    {
        List<string> Entitlements { get; }
        AccessTokenResponse Token { get; }

        Task<bool> InConfidentialWay(IConnectionSettings settings);
        Task<bool> InPublicWay(IConnectionSettings settings);
    }
}