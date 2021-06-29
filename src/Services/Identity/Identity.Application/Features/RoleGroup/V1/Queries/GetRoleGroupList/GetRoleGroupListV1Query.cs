using MediatR;
using System.Collections.Generic;

namespace Identity.Application.Features.RoleGroup.V1.Queries.GetRoleGroupList
{
    public class GetRoleGroupListV1Query : IRequest<List<RoleGroupV1Response>>
    {

    }
}
