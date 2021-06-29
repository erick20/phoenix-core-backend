using Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Contracts.Persistence.Base
{
    public interface IPermissionGroupRepositoryBase : IBaseRepository<PermissionGroup>
    {
        Task<PermissionGroup> GetPermissionGroupByIdAsync(int id, bool withActiveState);
        Task<PermissionGroup> GetPermissionGroupByNameAsync(string name, bool withActiveState);
    }
}
