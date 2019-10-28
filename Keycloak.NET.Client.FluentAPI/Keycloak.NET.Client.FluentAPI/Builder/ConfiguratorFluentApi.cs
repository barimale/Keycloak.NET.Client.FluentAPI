namespace Keycloak.NET.FluentAPI.Builder
{
    public class ContextFluentBuilder : ILoginAs, IUrl, IRealm, IClient
    {
        private readonly Context _beingConstructed;

        public ContextFluentBuilder(Context beingConstructed)
        {
            _beingConstructed = beingConstructed;
        }

        IClient IRealm.Realm(string realmName)
        {
            _beingConstructed.ConnectionSettings.Realm = realmName;
            return this;
        }

        IUrl ILoginAs.Credentials(string username, string password)
        {
            _beingConstructed.ConnectionSettings.Username = username;
            _beingConstructed.ConnectionSettings.Password = password;
            return this;
        }

        IRealm IUrl.Url(string url)
        {
            _beingConstructed.ConnectionSettings.Url = url;
            return this;
        }

        Context IClient.Client(string clientName)
        {
            _beingConstructed.ConnectionSettings.ClientName = clientName;
            return _beingConstructed;
        }

        Context IRealm.AllRealms()
        {
            return _beingConstructed;
        }

        Context IClient.Client(string id, string secret)
        {
            _beingConstructed.ConnectionSettings.ClientName = id;
            _beingConstructed.ConnectionSettings.ClientSecret = secret;

            return _beingConstructed; ;
        }
    }

    public interface ILoginAs
    {
        IUrl Credentials(string username, string password);
    }

    public interface IUrl
    {
        IRealm Url(string keycloackInstanceUrl);
    }

    public interface IRealm
    {
        IClient Realm(string realmName);
        Context AllRealms();
    }

    public interface IClient
    {
        Context Client(string id);
        Context Client(string id, string secret);
    }
}