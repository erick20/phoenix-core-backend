using MediatR;
using System.Collections.Generic;

namespace Identity.Application.Features.Permission.V1.Queries.GetPermissionList
{
    public class GetPermissionListV1Query : IRequest<List<PermissionV1Response>>
    {
    }
}
