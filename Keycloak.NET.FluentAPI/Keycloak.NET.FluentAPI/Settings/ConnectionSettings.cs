namespace Keycloak.NET.FluentAPI.Settings
{
    internal class ConnectionSettings : IConnectionSettings
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
        public string Realm { get; set; }
        public string ClientName { get; set; }
    }
}
