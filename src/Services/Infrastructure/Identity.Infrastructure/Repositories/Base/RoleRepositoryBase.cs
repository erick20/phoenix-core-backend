﻿using Identity.Application.Contracts.Persistence.Base;
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
    public class RoleRepositoryBase : BaseRepository<Role>, IRoleRepositoryBase
    {
        NpgsqlTransaction _transaction;

        public RoleRepositoryBase(IdentityDbContext dbContext, NpgsqlTransaction transaction) : base(dbContext)
        {
            _dbContext = dbContext;
            _transaction = transaction;
        }
    }
}
