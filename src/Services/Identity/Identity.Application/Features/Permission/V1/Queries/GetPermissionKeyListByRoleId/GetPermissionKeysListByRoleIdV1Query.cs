using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Permission.V1.Queries.GetPermissionKeyListByRoleId
{
    public class GetPermissionKeyListByRoleIdV1Query : IRequest<List<string>>
    {
        [JsonProperty("roleId")]
        public int RoleId { get; set; }
    }
}
