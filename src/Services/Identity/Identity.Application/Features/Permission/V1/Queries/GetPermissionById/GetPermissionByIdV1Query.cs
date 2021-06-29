using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Permission.V1.Queries.GetPermissionById
{
    public class GetPermissionByIdV1Query : IRequest<PermissionV1Response>
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
