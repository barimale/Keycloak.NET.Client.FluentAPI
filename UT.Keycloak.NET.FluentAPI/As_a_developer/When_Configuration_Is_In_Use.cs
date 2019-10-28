using Keycloak.NET.FluentAPI;
using Keycloak.NET.FluentAPI.Configure;
using NUnit.Framework;
using System.Threading.Tasks;

namespace UT.Keycloak.NET.FluentAPI.As_a_developer
{
    public class When_Configuration_Is_In_Use
    {
        private IRealmContext context;

        [SetUp]
        public void Setup()
        {
            context = RealmContext.Create()
                .WithCredentials(InputData.Username, InputData.Password)
                .Endpoint(InputData.Endpoint)
                .ToRealm(InputData.Realm)
                .ToClientName(InputData.ClientId);
        }

        [Test]
        public void I_d_like_to_cannot_create_client_with_insufficient_priviligies()
        {
            Assert.ThrowsAsync<Flurl.Http.FlurlHttpException>(async () => await CreateClientWithUnsofficientPriviligies());
        }

        private async Task CreateClientWithUnsofficientPriviligies()
        {
            //given
            var clients = context.Configurator.Clients;
            var clientId = "123s";

            //when
            await clients.Create(clientId, Protocol.OPENID_CONNECT);

            //than
        }
    }
}