using Keycloak.NET.FluentAPI.Settings;

namespace Keycloak.NET.FluentAPI
{
    public interface IContext
    {
        AccessType ProtocolAccessType { get; set; }
        IConnectionSettings ConnectionSettings { get; }
    }

    public enum AccessType
    {
        Public,
        Confidential,
        Bearer_only
    }
}