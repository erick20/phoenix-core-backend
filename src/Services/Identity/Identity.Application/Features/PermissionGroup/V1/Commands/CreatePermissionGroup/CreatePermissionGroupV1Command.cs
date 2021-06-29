using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.PermissionGroup.V1.Commands.CreatePermissionGroup
{
    public class CreatePermissionGroupV1Command : IRequest<PermissionGroupV1Response>
    {
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonIgnore]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
