using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.PermissionGroup.V1.Queries.GetPermissionGroupList
{
    public class GetPermissionGroupListV1Query : IRequest<List<PermissionGroupV1Response>>
    {
    }
}
