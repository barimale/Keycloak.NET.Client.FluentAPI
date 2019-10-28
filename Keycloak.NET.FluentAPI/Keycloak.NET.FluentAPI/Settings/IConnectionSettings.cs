namespace Keycloak.NET.FluentAPI.Settings
{
    public interface IConnectionSettings
    {
        string ClientName { get; set; }
        string Password { get; set; }
        string Realm { get; set; }
        string Url { get; set; }
        string Username { get; set; }
    }
}