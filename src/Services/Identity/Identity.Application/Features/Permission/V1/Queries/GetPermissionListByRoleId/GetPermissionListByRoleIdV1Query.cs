using MediatR;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Identity.Application.Features.Permission.V1.Queries.GetPermissionListByRoleId
{
    public  class GetPermissionListByRoleIdV1Query : IRequest<List<HasPermissionV1Response>>
    {
        [JsonProperty("roleId")]
        public int RoleId { get; set; }
    }
}
