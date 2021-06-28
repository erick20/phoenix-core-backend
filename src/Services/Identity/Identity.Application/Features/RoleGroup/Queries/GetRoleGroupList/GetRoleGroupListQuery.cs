using Identity.Application.Features.RolesGroup.Queries;
using MediatR;
using System.Collections.Generic;

namespace Identity.Application.Features.RoleGroup.Queries.GetRoleGroupList
{
    public class GetRoleGroupListQuery : IRequest<List<RoleGroupResponse>>
    {

    }
}
