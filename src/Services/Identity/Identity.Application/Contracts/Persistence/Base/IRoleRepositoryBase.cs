using Identity.Application.Features;
using Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Contracts.Persistence.Base
{
    public interface IRoleRepositoryBase : IBaseRepository<Role>
    {
        Task<Role> GetRoleLimitByIdAsync(int id, bool withActiveState = false);
        IQueryable<Role> GetRoleLimitByWarehouseTypeIdAsync(int warehouseTypeId, bool withActiveState = false);
        Task<Role> GetRoleLimitPermissionByIdAsync(int id, bool withActiveState = false);
        Task<PagedResponse<Role>> GetPagedRoleListAsync(int pn, int ps);
        IQueryable<Role> GetRoleListByIds(List<int> ids);
    }
}
