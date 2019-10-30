using Keycloak.NET.FluentAPI.Settings;

namespace Keycloak.NET.FluentAPI
{
    public interface IContext
    {
        string CertificatePath { get; set; }
        AccessType ProtocolAccessType { get; set; }
        IConnectionSettings ConnectionSettings { get; }

        enum AccessType
        {
            Public,
            Confidential,
            Bearer_only
        }
    }
}