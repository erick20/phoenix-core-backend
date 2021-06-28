using Identity.Application.Features.RolesGroup.Queries;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.RoleGroup.Commands.UpdateRoleGroup
{
    public class UpdateRoleGroupCommand : IRequest<RoleGroupResponse>
    {
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonIgnore]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
