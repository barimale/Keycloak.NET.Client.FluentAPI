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
            var context = Context.Create()
                .WithCredentials(InputData.Username, InputData.Password)
                .Endpoint(InputData.Endpoint)
                .AllRealms();

            //than
            Assert.NotNull(context);
        }

        [Test]
        public void I_d_like_to_have_access_to_submodules()
        {
            //given
            var context = Context.Create()
                .WithCredentials(InputData.Username, InputData.Password)
                .Endpoint(InputData.Endpoint)
                .AllRealms();

            //when
            var realm = context.Realm;

            //than
            Assert.NotNull(realm);
        }
    }
}
