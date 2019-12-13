using Keycloak.NET.FluentAPI;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace UT.Keycloak.NET.FluentAPI.As_a_user
{
    public class When_Manager_Is_In_Use
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
            var service = new AuthorizationManager();

            var context = Context.Create()
                .Credentials(InputData.Username, InputData.Password)
                .Url(InputData.Endpoint)
                .Realm(InputData.Realm)
                .OpenIdConnect()
                .Confidential(InputData.ClientId, InputData.ClientSecret);

            //when
            var result = await service
                .AuthorizeAsync(context)
                .ConfigureAwait(false);

            //then
            Assert.IsTrue(result);
            Assert.Greater(service.PriviligiesAsListOfNames().Count, 0);
            Assert.Greater(service.PriviligiesAsListOfRoles().Count, 0);
            Assert.NotNull(service.Token);
        }

        [Test]
        public async Task I_d_like_to_have_all_my_entitlements_downloaded_by_using_public_access_type()
        {
            //given
            var service = new AuthorizationManager();

            var context = Context.Create()
                .Credentials(InputData.Username, InputData.Password)
                .Url(InputData.Endpoint)
                .Realm(InputData.Realm)
                .OpenIdConnect()
                .Public(InputData.PublicClientId);

            //when
            var result = await service
                .AuthorizeAsync(context)
                .ConfigureAwait(false);

            //then
            Assert.IsTrue(result);
            Assert.AreEqual(service.PriviligiesAsListOfNames().Count, 1);
            Assert.AreEqual(service.PriviligiesAsListOfRoles().Count, 1);
            Assert.AreEqual(service.RealmPriviligiesAsListOfNames().Count, 2);
            Assert.AreEqual(service.RealmPriviligiesAsListOfRoles().Count, 2);
            Assert.NotNull(service.Token);
        }

        [Test]
        public void I_d_like_to_have_all_my_entitlements_downloaded_by_using_bearer_only_access_type()
        {
            //given
            var service = new AuthorizationManager();

            var context = Context.Create()
                .Credentials(InputData.Username, InputData.Password)
                .Url(InputData.Endpoint)
                .Realm(InputData.Realm)
                .OpenIdConnect()
                .BearerOnly(InputData.BearerOnlyClientId, InputData.BearerOnlyClientSecret);

            var ex = Assert.Throws<AggregateException>(() =>
            {
                //when
                var result = service.AuthorizeAsync(context).Result;
            });

            //then
            Assert.IsNotNull(ex);
            Assert.AreEqual(ex.InnerException.GetType(), typeof(NotImplementedException));
        }

        [Test]
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

            //then
            Assert.NotNull(context);
        }
    }
}
