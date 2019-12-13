namespace UT.Keycloak.NET.FluentAPI
{
    public static class InputData
    {
        public static string Username = "superuser";
        public static string Password = "demo";
        public static string Endpoint = "http://localhost:8080/";
        public static string Realm = "DEV";

        public static string ClientId = "dummy-confidential";
        public static string ClientSecret = "efdbb9ab-76da-42a0-8c36-fae07bba9128";

        public static string PublicClientId = "dummy-public-access-type";

        public static string BearerOnlyClientId = "dummy-bearer-only";
        public static string BearerOnlyClientSecret = "195cf823-324e-4dd2-8061-54085a3987bd";
    }
}