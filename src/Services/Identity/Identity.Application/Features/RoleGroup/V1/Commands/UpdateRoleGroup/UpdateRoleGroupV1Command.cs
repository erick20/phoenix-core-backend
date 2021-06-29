using MediatR;
using Newtonsoft.Json;

namespace Identity.Application.Features.RoleGroup.V1.Commands.UpdateRoleGroup
{
    public class UpdateRoleGroupV1Command : IRequest<RoleGroupV1Response>
    {
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonIgnore]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
