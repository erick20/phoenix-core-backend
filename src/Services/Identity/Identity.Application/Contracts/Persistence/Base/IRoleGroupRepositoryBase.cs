using Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Contracts.Persistence.Base
{
    public interface IRoleGroupRepositoryBase : IBaseRepository<RoleGroup>
    {
        Task<RoleGroup> GetRoleGroupByIdAsync(int id, bool withActiveState);
        Task<RoleGroup> GetRoleGroupByNameAsync(string name, bool withActiveState);
    }
}
