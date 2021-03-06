using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Permission.V1.Commands.CheckCustomerPermission
{
    public class CheckCustomerPermissionV1Command : IRequest<Unit>
    {
        [JsonProperty("roleId")]
        public int RoleId { get; set; }

        [JsonProperty("actionName")]
        public string ActionName { get; set; }
    }
}
