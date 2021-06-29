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
    public class PermissionGroupRepositoryBase : BaseRepository<PermissionGroup>, IPermissionGroupRepositoryBase
    {
        NpgsqlTransaction _transaction;

        public PermissionGroupRepositoryBase(IdentityDbContext dbContext, NpgsqlTransaction transaction) : base(dbContext)
        {
            _dbContext = dbContext;
            _transaction = transaction;
        }

        public async Task<PermissionGroup> GetPermissionGroupByIdAsync(int id, bool withActiveState)
        {
            var entity = withActiveState ? await Get(x => x.Id == id).FirstOrDefaultAsync() : await GetNoTracking(x => x.Id == id).FirstOrDefaultAsync();
            return entity;
        }

        public async Task<PermissionGroup> GetPermissionGroupByNameAsync(string name, bool withActiveState)
        {
            var entity = withActiveState ? await Get(x => x.Name == name).FirstOrDefaultAsync() : await GetNoTracking(x => x.Name == name).FirstOrDefaultAsync();
            return entity;
        }
    }
}
