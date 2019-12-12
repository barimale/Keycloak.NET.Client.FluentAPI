namespace UT.Keycloak.NET.FluentAPI
{
    public static class InputData
    {
        public static string Username = "foo";
        public static string Password = "bar";
        public static string Endpoint = "http://localhost:8080/";
        public static string Realm = "DEV";

        public static string ClientId = "Confidential";
        public static string ClientSecret = "8a03e418-bf2d-42a4-b9d1-7d8f1ed633d5";

        public static string PublicClientId = "PublicAccessType";

        public static string BearerOnlyClientId = "BearerOnly";
        public static string BearerOnlyClientSecret = "ebccd71e-36a4-441c-94ab-f729e1fcdc24";
    }
}