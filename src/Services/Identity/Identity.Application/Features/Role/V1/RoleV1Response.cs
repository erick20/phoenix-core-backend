using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Role.V1
{
    public class RoleV1Response
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("role_group_id")]
        public int RoleGroupId { get; set; }

        [JsonProperty("warehouseTypeId")]
        public int WarehouseTypeId { get; set; }
    }
}
