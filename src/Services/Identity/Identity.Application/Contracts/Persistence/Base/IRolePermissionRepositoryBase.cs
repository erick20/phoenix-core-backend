using Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Contracts.Persistence.Base
{
    public interface IRolePermissionRepositoryBase : IBaseRepository<RolePermission>
    {
        Task<RolePermission> GetByRoleIdActionNameAsync(int roleId, string actionName, bool withActiveState);
    }
}
