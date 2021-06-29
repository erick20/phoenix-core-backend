using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Permission.V1.Queries.GetPermissionListByRoleAndGroupId
{
    public class GetPermissionListByRoleAndGroupIdV1Query : IRequest<List<HasPermissionV1Response>>
    {
        [JsonProperty("roleId")]
        public int RoleId { get; set; }

        [JsonProperty("groupId")]
        public int GroupId { get; set; }
    }
}
