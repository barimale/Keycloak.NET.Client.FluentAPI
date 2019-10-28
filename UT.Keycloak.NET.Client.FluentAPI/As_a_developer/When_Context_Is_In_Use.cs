using Keycloak.NET.FluentAPI;
using NUnit.Framework;

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
                .Client(InputData.ClientId);

            var confidentialContext = Context.Create()
                .Credentials(InputData.Username, InputData.Password)
                .Url(InputData.Endpoint)
                .Realm(InputData.Realm)
                .Client(InputData.ClientId, InputData.ClientSecret);

            //than
            Assert.NotNull(publicContext);
            Assert.NotNull(confidentialContext);
        }
    }
}
