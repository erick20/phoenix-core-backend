using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.RolesGroup.Queries.GetRolesGroupById
{
    public class GetRoleGroupByIdQuery : IRequest<RoleGroupResponse>
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
