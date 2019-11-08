namespace Keycloak.NET.FluentAPI.Builder
{
    public class ContextFluentBuilder : ILoginAs, IUrl, IRealm, IClient, IProtocol, ISamlClient
    {
        private readonly Context _context;

        public ContextFluentBuilder(Context beingConstructed)
        {
            _context = beingConstructed;
        }

        IProtocol IRealm.Realm(string realmName)
        {
            _context.ConnectionSettings.Realm = realmName;

            return this;
        }

        IUrl ILoginAs.Credentials(string username, string password)
        {
            _context.ConnectionSettings.Username = username;
            _context.ConnectionSettings.Password = password;

            return this;
        }

        IRealm IUrl.Url(string url)
        {
            _context.ConnectionSettings.Url = url;

            return this;
        }

        Context IClient.Public(string clientName)
        {
            _context.ConnectionSettings.ClientName = clientName;
            _context.ProtocolAccessType = AccessType.Public;

            return _context;
        }

        Context IRealm.AllRealms()
        {
            return _context;
        }

        Context IClient.Confidential(string id, string secret)
        {
            _context.ConnectionSettings.ClientName = id;
            _context.ConnectionSettings.ClientSecret = secret;
            _context.ProtocolAccessType = AccessType.Confidential;

            return _context;
        }

        Context IClient.BearerOnly(string clientId, string secret)
        {
            _context.ConnectionSettings.ClientName = clientId;
            _context.ConnectionSettings.ClientSecret = secret;
            _context.ProtocolAccessType = AccessType.Bearer_only;

            return _context;
        }

        IClient IProtocol.OpenIdConnect()
        {
            _context.ProtocolType = ClientProtocolType.openIdConnect;

            return this;
        }

        ISamlClient IProtocol.Saml()
        {
            _context.ProtocolType = ClientProtocolType.saml;

            return this;
        }

        Context ISamlClient.Certificate(string pathToFile)
        {
            _context.CertificatePath = pathToFile;

            return _context;
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
        IProtocol Realm(string realmName);
        Context AllRealms();
    }

    public interface IProtocol
    {
        IClient OpenIdConnect();
        ISamlClient Saml();
    }

    public interface ISamlClient
    {
        Context Certificate(string pathToFile);
    }

    public interface IClient
    {
        Context Public(string clientId);
        Context Confidential(string clientId, string secret);
        Context BearerOnly(string clientId, string secret);
    }
}