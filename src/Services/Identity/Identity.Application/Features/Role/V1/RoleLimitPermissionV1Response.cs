using Identity.Application.Features.Permission.V1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Role.V1
{
    public class RoleLimitPermissionV1Response : RoleV1Response
    {
        [JsonProperty("accountLimits")]
        List<AccountLimitV1Reposne> AccountLimits { get; set; }

        [JsonProperty("permissions")]
        List<PermissionV1Response> Permissions { get; set; }
    }
}
