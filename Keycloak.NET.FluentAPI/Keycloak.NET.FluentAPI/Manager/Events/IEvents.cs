namespace Keycloak.NET.FluentAPI.Manage.Events
{
    public interface IEvents
    {
        IAdminEvents AdminEvents { get; }
        ILoginEvents LoginEvents { get; }
        IConfig Config { get; }
    }
}