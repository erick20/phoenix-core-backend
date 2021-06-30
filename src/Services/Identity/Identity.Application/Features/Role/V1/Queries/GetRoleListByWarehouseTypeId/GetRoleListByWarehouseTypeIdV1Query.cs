using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Role.V1.Queries.GetRoleListByWarehouseTypeId
{
    public class GetRoleListByWarehouseTypeIdV1Query : IRequest<List<RoleLimitPermissionV1Response>>
    {
        [JsonProperty("warehouseTypeId")]
        public int WarehouseTypeId { get; set; }
    }
}
