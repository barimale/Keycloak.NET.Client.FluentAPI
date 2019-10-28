namespace Keycloak.NET.FluentAPI.Manage.Sessions
{
    public interface ISession
    {
        IRealmSessions RealmSessions { get; }
        IRevocation Revocation { get; }
    }
}