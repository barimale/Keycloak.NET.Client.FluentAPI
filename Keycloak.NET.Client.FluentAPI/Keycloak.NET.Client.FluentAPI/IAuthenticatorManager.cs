using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Keycloak.NET.FluentAPI.Model;
using Keycloak.NET.FluentAPI.Settings;

namespace Keycloak.NET.FluentAPI
{
    public interface IAuthenticatorManager
    {
        string UserId { get; }
        List<string> Entitlements { get; }
        AccessTokenResponse Token { get; }

        Task<bool> InConfidentialWay(IConnectionSettings settings, CancellationToken token = default);
        Task<bool> InPublicWay(IConnectionSettings settings, CancellationToken token = default);
    }
}