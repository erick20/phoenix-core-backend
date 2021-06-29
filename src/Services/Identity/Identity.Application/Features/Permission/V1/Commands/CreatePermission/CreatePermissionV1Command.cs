using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Permission.V1.Commands.CreatePermission
{
    public class CreatePermissionV1Command : IRequest<PermissionV1Response>
    {
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonIgnore]
        public int Id { get; set; }

        [JsonProperty("actionKey")]
        public string ActionKey { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("groupId")]
        public int GroupId { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
    }
}
