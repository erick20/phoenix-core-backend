using Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Contracts.Persistence.Base
{
    public interface IPermissionRepositoryBase : IBaseRepository<Permission>
    {
        Task<Permission> GetPermissionByIdAsync(int id, bool withActiveState);
        IQueryable<Permission> GetPermissionByGroupId(int groupId);
    }
}
