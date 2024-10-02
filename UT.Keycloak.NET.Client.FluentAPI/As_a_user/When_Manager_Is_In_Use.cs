using Keycloak.NET.FluentAPI;
using NUnit.Framework;
using NUnit.Framework.Legacy;
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
            ClassicAssert.IsTrue(result);
            ClassicAssert.Greater(service.PriviligiesAsListOfNames().Count, 0);
            ClassicAssert.Greater(service.PriviligiesAsListOfRoles().Count, 0);
            ClassicAssert.NotNull(service.Token);
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
            ClassicAssert.IsTrue(result);
            ClassicAssert.AreEqual(service.PriviligiesAsListOfNames().Count, 1);
            ClassicAssert.AreEqual(service.PriviligiesAsListOfRoles().Count, 1);
            ClassicAssert.AreEqual(service.RealmPriviligiesAsListOfNames().Count, 2);
            ClassicAssert.AreEqual(service.RealmPriviligiesAsListOfRoles().Count, 2);
            ClassicAssert.NotNull(service.Token);
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
            ClassicAssert.IsNotNull(ex);
            ClassicAssert.AreEqual(ex.InnerException.GetType(), typeof(NotImplementedException));
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
            ClassicAssert.NotNull(context);
        }
    }
}
