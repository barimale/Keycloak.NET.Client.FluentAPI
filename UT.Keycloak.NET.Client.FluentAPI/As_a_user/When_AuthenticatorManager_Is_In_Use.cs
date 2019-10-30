using Keycloak.NET.FluentAPI;
using NUnit.Framework;
using System.Threading.Tasks;

namespace UT.Keycloak.NET.FluentAPI.As_a_user
{
    public class When_AuthenticatorManager_Is_In_Use
    {
        [SetUp]
        public void Setup()
        {
            //intentionally left blank
        }

        [Test]
        public async Task I_d_like_to_have_all_my_entitlements_downloaded_by_using_confidential_access_type()
        {
            //given
            var service = new AuthenticatorManager();

            var context = Context.Create()
                .Credentials(InputData.Username, InputData.Password)
                .Url(InputData.Endpoint)
                .Realm(InputData.Realm)
                .OpenIdConnect()
                .Confidential(InputData.ClientId, InputData.ClientSecret);

            //when
            var result = await service
                .Authorize(context)
                .ConfigureAwait(false);

            //than
            Assert.IsTrue(result);
            Assert.Greater(service.Priviligies.Count, 0);
            Assert.NotNull(service.Token);
        }

        [Test]
        public async Task I_d_like_to_have_all_my_entitlements_downloaded_by_using_public_access_type()
        {
            //given
            var service = new AuthenticatorManager();

            var context = Context.Create()
                .Credentials(InputData.Username, InputData.Password)
                .Url(InputData.Endpoint)
                .Realm(InputData.Realm)
                .OpenIdConnect()
                .Public(InputData.PublicClientId);

            //when
            var result = await service
                .Authorize(context)
                .ConfigureAwait(false);

            //than
            Assert.IsTrue(result);
            Assert.Greater(service.Priviligies.Count, 0);
            Assert.NotNull(service.Token);
        }

        [Test]
        public async Task I_d_like_to_have_all_my_entitlements_downloaded_by_using_bearer_only_access_type()
        {
            //given
            var service = new AuthenticatorManager();

            var context = Context.Create()
                .Credentials(InputData.Username, InputData.Password)
                .Url(InputData.Endpoint)
                .Realm(InputData.Realm)
                .OpenIdConnect()
                .BearerOnly(InputData.BearerOnlyClientId, InputData.BearerOnlyClientSecret);

            //when
            var result = await service
                .Authorize(context)
                .ConfigureAwait(false);

            //than
            Assert.IsTrue(result);
            Assert.Greater(service.Priviligies.Count, 0);
            Assert.NotNull(service.Token);
        }

        public void I_d_like_to_have_context_with_saml_configuration()
        {
            //given

            //when
            var context = Context.Create()
                .Credentials(InputData.Username, InputData.Password)
                .Url(InputData.Endpoint)
                .Realm(InputData.Realm)
                .Saml()
                .Certificate(string.Empty);

            //than
            Assert.NotNull(context);
        }
    }
}
