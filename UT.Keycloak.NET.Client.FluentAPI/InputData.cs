namespace UT.Keycloak.NET.FluentAPI
{
    public static class InputData
    {
        public static string Username = "superuser";
        public static string Password = "demo";
        public static string Endpoint = "http://localhost:8080/";
        public static string Realm = "DEV";

        public static string ClientId = "SoundManager-WPF";
        public static string ClientSecret = "223e036a-fe9d-4963-b198-956c99316270";

        public static string PublicClientId = "publicAccessType";

        public static string BearerOnlyClientId = "BearerOnly";
        public static string BearerOnlyClientSecret = "d0d4ebd0-2a49-4ba2-accb-9c603cbded88";
    }
}