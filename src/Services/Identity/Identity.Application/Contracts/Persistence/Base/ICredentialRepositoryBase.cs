using Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Contracts.Persistence.Base
{
    public interface ICredentialRepositoryBase : IBaseRepository<Credential>
    {
        Task<Credential> GetCredetialByIdAsync(int credentialId, bool withActiveState = false);
        Task<Credential> GetCredetialByIdAndPasswordAsync(int credentialId, string password, bool withActiveState = false);
    }
}
