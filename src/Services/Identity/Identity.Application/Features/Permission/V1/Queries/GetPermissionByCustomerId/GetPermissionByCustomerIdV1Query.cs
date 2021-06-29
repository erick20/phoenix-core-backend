using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Permission.V1.Queries.GetPermissionByCustomerId
{
    public class GetPermissionByCustomerIdV1Query : IRequest<List<PermissionV1Response>>
    {
        [JsonProperty("customerId")]
        public int CustomerId { get; set; }
    }
}
