using Keycloak.NET.FluentAPI.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Keycloak.NET.FluentAPI
{
    public interface IAuthenticatorManager
    {
        string UserId { get; }
        List<string> Priviligies { get; }
        AccessTokenResponse Token { get; }
        Task<bool> Authorize(IContext context, CancellationToken token = default);
    }
}