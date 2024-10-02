using Keycloak.NET.FluentAPI;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace UT.Keycloak.NET.FluentAPI.As_a_developer
{
    public class When_Context_Is_In_Use
    {
        [SetUp]
        public void Setup()
        {
            //intentionally left blank
        }

        [Test]
        public void I_d_Like_have_context_correctly_created()
        {
            //given

            //when
            var publicContext = Context.Create()
                .Credentials(InputData.Username, InputData.Password)
                .Url(InputData.Endpoint)
                .Realm(InputData.Realm)
                .OpenIdConnect()
                .Public(InputData.ClientId);

            var confidentialContext = Context.Create()
                .Credentials(InputData.Username, InputData.Password)
                .Url(InputData.Endpoint)
                .Realm(InputData.Realm)
                .OpenIdConnect()
                .Confidential(InputData.ClientId, InputData.ClientSecret);

            var bearerOnlyContext = Context.Create()
                .Credentials(InputData.Username, InputData.Password)
                .Url(InputData.Endpoint)
                .Realm(InputData.Realm)
                .OpenIdConnect()
                .BearerOnly(InputData.ClientId, InputData.ClientSecret);

            //than
            ClassicAssert.NotNull(publicContext);
            ClassicAssert.NotNull(confidentialContext);
            ClassicAssert.NotNull(bearerOnlyContext);
        }
    }
}