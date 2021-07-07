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
    public class CredentialRepositoryBase : BaseRepository<Credential>, ICredentialRepositoryBase
    {
        NpgsqlTransaction _transaction;

        public CredentialRepositoryBase(IdentityDbContext dbContext, NpgsqlTransaction transaction) : base(dbContext)
        {
            _dbContext = dbContext;
            _transaction = transaction;
        }

        public async Task<Credential> GetCredetialByIdAndPasswordAsync(int credentialId, string password, bool withActiveState)
        {
            var entity = withActiveState ? await Get(x => x.Id == credentialId && x.Password == password).FirstOrDefaultAsync()
                : await GetNoTracking(x => x.Id == credentialId && x.Password == password).FirstOrDefaultAsync();
            return entity;
        }

        public async Task<Credential> GetCredetialByIdAsync(int credentialId, bool withActiveState)
        {
            var entity = withActiveState ? await Get(x => x.Id == credentialId).FirstOrDefaultAsync()
                : await GetNoTracking(x => x.Id == credentialId).FirstOrDefaultAsync();
            return entity;
        }


    }
}
