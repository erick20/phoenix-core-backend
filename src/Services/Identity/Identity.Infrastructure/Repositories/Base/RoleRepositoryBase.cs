using Identity.Application.Contracts.Persistence.Base;
using Identity.Application.Features;
using Identity.Domain.Entities;
using Identity.Infrastructure.Extensions;
using Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Repositories.Base
{
    public class RoleRepositoryBase : BaseRepository<Role>, IRoleRepositoryBase
    {
        NpgsqlTransaction _transaction;

        public RoleRepositoryBase(IdentityDbContext dbContext, NpgsqlTransaction transaction) : base(dbContext)
        {
            _dbContext = dbContext;
            _transaction = transaction;
        }

        public async Task<PagedResponse<Role>> GetPagedRoleListAsync(int pn, int ps)
        {
            var entityList = await GetNoTracking().GetPagedAsync(pn, ps);

            return entityList;
        }

        public async Task<Role> GetRoleLimitByIdAsync(int id, bool withActiveState)
        {
            var entity = withActiveState ? await Get(x => x.Id == id, "Limits").FirstOrDefaultAsync()
                : await GetNoTracking(x => x.Id == id, "Limits").FirstOrDefaultAsync();
            return entity;
        }

        public IQueryable<Role> GetRoleLimitByWarehouseTypeIdAsync(int warehouseTypeId, bool withActiveState)
        {
            var entity = withActiveState ? Get(x => x.WarehouseTypeId == warehouseTypeId, "Limits")
                : GetNoTracking(x => x.WarehouseTypeId == warehouseTypeId, "Limits");
            return entity;
        }

        public async Task<Role> GetRoleLimitPermissionByIdAsync(int id, bool withActiveState)
        {
            var entity = withActiveState ? await Get(x => x.Id == id, "Limits", "RolePermissions").FirstOrDefaultAsync()
                : await GetNoTracking(x => x.Id == id, "Limits", "RolePermissions").FirstOrDefaultAsync();
            return entity;
        }

        public IQueryable<Role> GetRoleListByIds(List<int> ids)
        {
            var entityList = GetNoTracking(x => ids.Contains(x.Id));

            return entityList;
        }
    }
}
