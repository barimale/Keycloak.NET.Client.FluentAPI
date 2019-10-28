using System.Threading.Tasks;

namespace Keycloak.NET.FluentAPI.Configure
{
    public interface IClients
    {
        Task<bool> Create(string clientId, Protocol protocolType, string endpoint = "");
    }

    public enum Protocol
    {
        OPENID_CONNECT,
        SAML
    }
}