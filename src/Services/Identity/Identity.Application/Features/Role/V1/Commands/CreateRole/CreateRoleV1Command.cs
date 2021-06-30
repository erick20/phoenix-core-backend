using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Role.V1.Commands.CreateRole
{
    public class CreateRoleV1Command : IRequest<RoleLimitPermissionV1Response>
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("roleGroupId")]
        public int RoleGroupId { get; set; }

        [JsonProperty("warehouseTypeId")]
        public int WarehouseTypeId { get; set; }

        [JsonProperty("AccountLimits")]
        public List<AccountLimitV1> AccountLimits { get; set; }

    }
}
