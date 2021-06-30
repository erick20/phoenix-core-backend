using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Role.V1.Queries.GetRoleListPaged
{
    public class GetRoleListPagedV1Query : IRequest<PagedResponse<RoleV1Response>>
    {
        [JsonProperty("pn")]
        public int Pn { get; set; } = 1;

        [JsonProperty("ps")]
        public int Ps { get; set; } = 100;
    }
}
