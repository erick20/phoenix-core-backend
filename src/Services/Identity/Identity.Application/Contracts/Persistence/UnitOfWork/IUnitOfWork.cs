using Identity.Application.Contracts.Persistence.Base;
using Identity.Application.Contracts.Persistence.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Contracts.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        #region Credential
        public ICredentialRepositoryBase CredentialRepositoryBase { get; }
        public ICredentialRepositoryV1 CredentialRepositoryV1 { get; }
        #endregion

        #region Limit
        public ILimitRepositoryBase LimitRepositoryBase { get; }
        public ILimitRepositoryV1 LimitRepositoryV1 { get; }
        #endregion

        #region Permission
        public IPermissionRepositoryBase PermissionRepositoryBase { get; }
        public IPermissionRepositoryV1 PermissionRepositoryV1 { get; }
        #endregion


        #region PermissionGroup
        public IPermissionGroupRepositoryBase PermissionGroupRepositoryBase { get; }
        public IPermissionGroupRepositoryV1 PermissionGroupRepositoryV1 { get; }
        #endregion


        #region Role
        public IRoleRepositoryBase RoleRepositoryBase { get; }
        public IRoleRepositoryV1 RoleRepositoryV1 { get; }
        #endregion

        #region RoleGroup
        public IRoleGroupRepositoryBase RoleGroupRepositoryBase { get; }
        public IRoleGroupRepositoryV1 RoleGroupRepositoryV1 { get; }
        #endregion

        #region RolePermission
        public IRolePermissionRepositoryBase RolePermissionRepositoryBase { get; }
        public IRolePermissionRepositoryV1 RolePermissionRepositoryV1 { get; }
        #endregion

        #region SaveChanges
        Task<int> SaveChangesAsync();
        int SaveChanges();
        #endregion

        #region EF Transactions
        Task BeginEFTransactionAsync();

        void CommitEFTrasnaction();

        Task RollBackEFTrasnactionAsync();
        #endregion

        #region Transactions
        //Task<NpgsqlTransaction> BeginTransaction();

        //Task CommitTrasnactionAsync();

        //Task RollBackTrasnactionAsync();
        #endregion
    }
}
