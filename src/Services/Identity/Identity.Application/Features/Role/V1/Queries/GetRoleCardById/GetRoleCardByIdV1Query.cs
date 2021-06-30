using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Role.V1.Queries.GetRoleCardById
{
    public class GetRoleCardByIdV1Query : IRequest<RoleLimitPermissionV1Response>
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
