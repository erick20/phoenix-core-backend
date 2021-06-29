using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Permission.V1.Queries.GetPermissionListByGroupId
{
    public class GetPermissionListByGroupIdV1Query : IRequest<List<PermissionV1Response>>
    {
        [JsonProperty("groupId")]
        public int GroupId { get; set; }
    }
   
}
