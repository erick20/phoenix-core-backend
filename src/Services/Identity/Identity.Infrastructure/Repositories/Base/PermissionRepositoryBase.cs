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
    public class PermissionRepositoryBase : BaseRepository<Permission>, IPermissionRepositoryBase
    {
        NpgsqlTransaction _transaction;

        public PermissionRepositoryBase(IdentityDbContext dbContext, NpgsqlTransaction transaction) : base(dbContext)
        {
            _dbContext = dbContext;
            _transaction = transaction;
        }

        public async Task<Permission> GetPermissionByIdAsync(int id, bool withActiveState)
        {
            var entity = withActiveState ? await Get(x => x.Id == id).FirstOrDefaultAsync() : await GetNoTracking(x => x.Id == id).FirstOrDefaultAsync();
            return entity;
        }

       

        public IQueryable<Permission> GetPermissionByGroupId(int groupId)
        {
            var entityList = GetNoTracking(x => x.GroupId == groupId);
            return entityList;
        }

        public async Task<List<string>> GetPermissionKeyListByRoleIdAsync(int roleId)
        {
            var keyList = await GetNoTracking(x => x.RolePermissions.Any(y => y.RoleId == roleId)).Select(x => x.Key).ToListAsync();

            return keyList;
        }
    }
}
