using Keycloak.NET.FluentAPI.Builder;
using Keycloak.NET.FluentAPI.Settings;

namespace Keycloak.NET.FluentAPI
{
    public class Context : IContext
    {
        public string CertificatePath { get; set; }
        public IContext.AccessType ProtocolAccessType { get; set; }
        public IConnectionSettings ConnectionSettings { get; private set; } = new ConnectionSettings();
        public IContext.ClientProtocolType ProtocolType { get; set; }

        private Context()
        {
            //intentionally left blank
        }

        public static ILoginAs Create()
        {
            return new ContextFluentBuilder(new Context());
        }
    }
}