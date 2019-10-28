using Keycloak.Net;
using Keycloak.NET.FluentAPI.Settings;
using System.Threading.Tasks;

namespace Keycloak.NET.FluentAPI.Base
{
    public abstract class BaseContext : IBaseContext
    {
        public IConnectionSettings ConnectionSettings { get; private set; } = new ConnectionSettings();

        public KeycloakClient Client { get; private set; }

        internal virtual async Task<BaseContext> Connect()
        {
            try
            {
                Client = new KeycloakClient(
                    ConnectionSettings.Url,
                    ConnectionSettings.Username,
                    ConnectionSettings.Password);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            return this;
        }
    }
}
