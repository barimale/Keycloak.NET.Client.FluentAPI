using Keycloak.NET.FluentAPI.Base;

namespace Keycloak.NET.FluentAPI.Builder
{
    public class ContextFluentBuilder : ILoginAs, IUrl, IRealm, IClient
    {
        private readonly BaseContext _beingConstructed;

        public ContextFluentBuilder(BaseContext beingConstructed)
        {
            _beingConstructed = beingConstructed;
        }

        IClient IRealm.ToRealm(string realmName)
        {
            _beingConstructed.ConnectionSettings.Realm = realmName;
            return this;
        }

        IUrl ILoginAs.WithCredentials(string username, string password)
        {
            _beingConstructed.ConnectionSettings.Username = username;
            _beingConstructed.ConnectionSettings.Password = password;
            return this;
        }

        IRealm IUrl.Endpoint(string url)
        {
            _beingConstructed.ConnectionSettings.Url = url;
            return this;
        }

        RealmContext IClient.ToClientName(string clientName)
        {
            _beingConstructed.ConnectionSettings.ClientName = clientName;
            return _beingConstructed
                .Connect()
                .Result as RealmContext;
        }

        Context IRealm.AllRealms()
        {
            return _beingConstructed
                .Connect()
                .Result as Context;
        }
    }

    public interface ILoginAs
    {
        IUrl WithCredentials(string username, string password);
    }

    public interface IUrl
    {
        IRealm Endpoint(string keycloackInstanceUrl);
    }

    public interface IRealm
    {
        IClient ToRealm(string realmName);
        Context AllRealms();
    }

    public interface IClient
    {
        RealmContext ToClientName(string clientName);
    }
}