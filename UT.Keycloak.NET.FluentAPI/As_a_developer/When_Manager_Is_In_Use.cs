using Keycloak.NET.FluentAPI;
using NUnit.Framework;
using System.Threading.Tasks;

namespace UT.Keycloak.NET.FluentAPI.As_a_developer
{
    public class When_Manager_Is_In_Use
    {
        [SetUp]
        public void Setup()
        {
            //intentionally left blank
        }

        [Test]
        public async Task I_d_like_to_import_realm()
        {
            //given
            var context = RealmContext.Create()
               .WithCredentials(InputData.Username, InputData.Password)
               .Endpoint(InputData.Endpoint)
               .ToRealm(InputData.Realm)
               .ToClientName(InputData.ClientId);

            var realm = await context.Manager.Export.ExportAsync(true, true);

            //when
            var result = await context.Manager.Import.ImportAsync(realm);

            //than
            Assert.IsTrue(result);
        }

        [Test]
        public async Task I_d_like_to_export_realm()
        {
            //given
            var context = RealmContext.Create()
               .WithCredentials(InputData.Username, InputData.Password)
               .Endpoint(InputData.Endpoint)
               .ToRealm(InputData.Realm)
               .ToClientName(InputData.ClientId);

            //when
            var result = await context.Manager.Export.ExportAsync(true, true);

            //than
            Assert.NotNull(result);
        }

        [Test]
        public async Task I_d_like_to_export_realm_as_byte_array()
        {
            //given
            var context = RealmContext.Create()
               .WithCredentials(InputData.Username, InputData.Password)
               .Endpoint(InputData.Endpoint)
               .ToRealm(InputData.Realm)
               .ToClientName(InputData.ClientId);

            //when
            var result = await context.Manager.Export.ExportToByteArrayAsync(true, true);

            //than
            Assert.NotNull(result);
        }
    }
}
