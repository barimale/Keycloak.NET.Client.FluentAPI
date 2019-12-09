using Keycloak.Net.Models.Roles;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Keycloak.NET.Client.FluentAPI.Model
{
    [JsonObject]
    public class AtributedRole
    {
        [JsonProperty("role")]
        public Role Role { get; set; }
        [JsonProperty("attributes")]
        public Dictionary<string,string> Attributes { get; set; }
    }
}
