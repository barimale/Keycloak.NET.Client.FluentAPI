﻿using Keycloak.NET.FluentAPI;
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

            var confidentialContext = Context.Create()
                .Credentials(InputData.Username, InputData.Password)
                .Url(InputData.Endpoint)
                .Realm(InputData.Realm)
                .Confidential(InputData.ClientId, InputData.ClientSecret);

            //when
            var result = await service
                .Authorize(confidentialContext)
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

            var confidentialContext = Context.Create()
                .Credentials(InputData.Username, InputData.Password)
                .Url(InputData.Endpoint)
                .Realm(InputData.Realm)
                .Public(InputData.PublicClientId);

            //when
            var result = await service
                .Authorize(confidentialContext)
                .ConfigureAwait(false);

            //than
            Assert.IsTrue(result);
            Assert.Greater(service.Priviligies.Count, 0);
            Assert.NotNull(service.Token);
        }
    }
}
