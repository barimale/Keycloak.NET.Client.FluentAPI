using Newtonsoft.Json;
using System.Collections.Generic;

namespace Keycloak.NET.FluentAPI.Model
{
    [JsonObject]
    public class ClientRoles
    {
        [JsonProperty]
        public Dictionary<string, RealmRoles> RolesPerClient { get; set; } = new Dictionary<string, RealmRoles>();
    }
}
