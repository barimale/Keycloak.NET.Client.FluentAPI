using Keycloak.NET.FluentAPI;
using NUnit.Framework;

namespace UT.Keycloak.NET.FluentAPI.As_a_user
{
    public class When_Authenticator_Is_In_Use
    {
        [SetUp]
        public void Setup()
        {
            //intentionally left blank
        }

        [Test]
        public void I_d_like_to_have_all_my_entitlements_downloaded()
        {
            //given
            var service = new Authenticator(
                InputData.Username, 
                InputData.Password, 
                InputData.Endpoint,
                InputData.Realm,
                InputData.ClientId,
                InputData.ClientSecret);

            //when
            var result = service.AuthenticatePublic();

            //than
            Assert.IsTrue(result);
        }

        [Test]
        public void I_d_like_to_have_all_my_entitlements_downloaded_in_confidential_way()
        {
            //given
            var service = new Authenticator(
                InputData.Username,
                InputData.Password,
                InputData.Endpoint,
                InputData.Realm,
                InputData.ClientId,
                InputData.ClientSecret);

            //when
            var result = service.AuthenticateConfidential();

            //than
            Assert.IsTrue(result);
        }
    }
}
