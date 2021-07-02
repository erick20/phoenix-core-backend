using Identity.Application.Contracts.Persistence.Base;
using Identity.Domain.Entities;
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
    public class RolePermissionRepositoryBase : BaseRepository<RolePermission>, IRolePermissionRepositoryBase
    {
        NpgsqlTransaction _transaction;

        public RolePermissionRepositoryBase(IdentityDbContext dbContext, NpgsqlTransaction transaction) : base(dbContext)
        {
            _dbContext = dbContext;
            _transaction = transaction;
        }

        public async Task<RolePermission> GetByRoleIdActionNameAsync(int roleId, string actionName, bool withActiveState)
        {
            var entity = withActiveState ? await Get(x => x.RoleId == roleId && x.Permission.ActionKey == actionName).FirstOrDefaultAsync()
                                        : await GetNoTracking(x => x.RoleId == roleId && x.Permission.ActionKey == actionName).FirstOrDefaultAsync();
            return entity;
        }
    }
}
