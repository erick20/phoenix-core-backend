using Identity.Application.Contracts.Persistence.Base;
using Identity.Domain.Entities;
using Identity.Infrastructure.Persistence;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Repositories.Base
{
    public class PermissionRepositoryV1 : PermissionRepositoryBase, IPermissionRepositoryV1
    {
        NpgsqlTransaction _transaction;

        public PermissionRepositoryV1(IdentityDbContext dbContext, NpgsqlTransaction transaction) : base(dbContext, transaction)
        {
            _dbContext = dbContext;
            _transaction = transaction;
        }
    }
}
