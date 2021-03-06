using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Permission.V1
{
    public class PermissionV1Response
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("actionKey")]
        public string ActionKey { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("groupId")]
        public int GroupId { get; set; }

        [JsonProperty("groupName")]
        public string GroupName { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
    }
}
