using Keycloak.Net.Models.Users;
using Keycloak.NET.FluentAPI.Base;
using Keycloak.NET.FluentAPI.Builder;
using Keycloak.NET.FluentAPI.Configure;
using Keycloak.NET.FluentAPI.Manage;
using System.Linq;
using System.Threading.Tasks;

namespace Keycloak.NET.FluentAPI
{
    public sealed class RealmContext : BaseContext, IRealmContext
    {
        private RealmContext()
            : base()
        {
            //intentionally left blank
        }

        public IManager Manager => new Manager(this);
        public IConfigurator Configurator => new Configurator(this);

        public User UserDetails { get; private set; }

        internal override async Task<BaseContext> Connect()
        {
            try
            {
                await base.Connect();

                var users = await Client.GetUsersAsync(ConnectionSettings.Realm);

                UserDetails = users.FirstOrDefault(p => p.UserName == ConnectionSettings.Username);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            return this;
        }

        public static ILoginAs Create()
        {
            return new ContextFluentBuilder(new RealmContext());
        }
    }
}
