using Keycloak.NET.FluentAPI;
using NUnit.Framework;

namespace UT.Keycloak.NET.FluentAPI.As_a_developer
{
    public class When_RealmContext_Is_In_Use
    {
        [SetUp]
        public void Setup()
        {
            //intentionally left blank
        }

        [Test]
        public void I_d_Like_have_content_correctly_created()
        {
            //given

            //when
            var content = RealmContext.Create()
                .WithCredentials(InputData.Username, InputData.Password)
                .Endpoint(InputData.Endpoint)
                .ToRealm(InputData.Realm)
                .ToClientName(InputData.ClientId);

            //than
            Assert.NotNull(content);
        }

        [Test]
        public void I_d_like_to_have_access_to_submodules()
        {
            //given
            var content = RealmContext.Create()
                .WithCredentials(InputData.Username, InputData.Password)
                .Endpoint(InputData.Endpoint)
                .ToRealm(InputData.Realm)
                .ToClientName(InputData.ClientId);

            //when
            var configuration = content.Configurator;
            var manager = content.Manager;
            var groups = content.Manager.Groups;
            var clients = content.Configurator.Clients;
            var clientScopes = content.Configurator.ClientScopes;

            //than
            Assert.NotNull(configuration);
            Assert.NotNull(manager);
            Assert.NotNull(groups);
            Assert.NotNull(clients);
            Assert.NotNull(clientScopes);
        }
    }
}
