namespace Keycloak.NET.FluentAPI.Configure
{
    public interface IConfigurator
    {
        IClients Clients { get; }
        IClientScopes ClientScopes { get; }
        IRoles Roles { get; }
        IIdentityProviders IdentityProviders { get; }
        IRealmSettings RealmSettings { get; }
    }
}