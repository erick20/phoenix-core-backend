using Identity.Application.Contracts.Persistence.Base;
using Identity.Application.Contracts.Persistence.UnitOfWork;
using Identity.Application.Contracts.Persistence.V1;
using Identity.Application.Exceptions;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields
        ICredentialRepositoryBase _credentialRepositoryBase;
        ICredentialRepositoryV1 _credentialRepositoryV1;
        ILimitRepositoryBase _limitRepositoryBase;
        ILimitRepositoryV1 _limitRepositoryV1;
        IPermissionRepositoryBase _permissionRepositoryBase;
        IPermissionRepositoryV1 _permissionRepositoryV1;
        IPermissionGroupRepositoryBase _permissionGroupRepositoryBase;
        IPermissionGroupRepositoryV1 _permissionGroupRepositoryV1;
        IRoleRepositoryBase _roleRepositoryBase;
        IRoleRepositoryV1 _roleRepositoryV1;
        IRoleGroupRepositoryBase _roleGroupRepositoryBase;
        IRoleGroupRepositoryV1 _roleGroupRepositoryV1;
        IRolePermissionRepositoryBase _rolePermissionRepositoryBase;
        IRolePermissionRepositoryV1 _rolePermissionRepositoryV1;

        private IdentityDbContext _dbContext;
        private IDbContextTransaction _efTransaction;
        //protected IConfiguration _configuration;
        #endregion

        #region Ctor
        public UnitOfWork(IdentityDbContext dbContext)//, IConfiguration configuration)
        {
            _dbContext = dbContext;
            //_configuration = configuration;
        }
        #endregion

        #region Repositories

        #region Credential Repository
        public ICredentialRepositoryBase CredentialRepositoryBase
        {
            get { return _credentialRepositoryBase ??= new CredentialRepositoryBase(_dbContext, _transaction); }
        }

        public ICredentialRepositoryV1 CredentialRepositoryV1
        {
            get { return _credentialRepositoryV1 ??= new CredentialRepositoryV1(_dbContext, _transaction); }
        }

        #endregion

        #region Limit Repository
        public ILimitRepositoryBase LimitRepositoryBase
        {
            get { return _limitRepositoryBase ??= new LimitRepositoryBase(_dbContext, _transaction); }
        }

        public ILimitRepositoryV1 LimitRepositoryV1
        {
            get { return _limitRepositoryV1 ??= new LimitRepositoryV1(_dbContext, _transaction); }
        }

        #endregion

        #region Permission Repository
        public IPermissionRepositoryBase PermissionRepositoryBase
        {
            get { return _permissionRepositoryBase ??= new PermissionRepositoryBase(_dbContext, _transaction); }
        }

        public IPermissionRepositoryV1 PermissionRepositoryV1
        {
            get { return _permissionRepositoryV1 ??= new PermissionRepositoryV1(_dbContext, _transaction); }
        }

        #endregion

        #region PermissionGroup Repository
        public IPermissionGroupRepositoryBase PermissionGroupRepositoryBase
        {
            get { return _permissionGroupRepositoryBase ??= new PermissionGroupRepositoryBase(_dbContext, _transaction); }
        }

        public IPermissionGroupRepositoryV1 PermissionGroupRepositoryV1
        {
            get { return _permissionGroupRepositoryV1 ??= new PermissionGroupRepositoryV1(_dbContext, _transaction); }
        }

        #endregion

        #region Role Repository
        public IRoleRepositoryBase RoleRepositoryBase
        {
            get { return _roleRepositoryBase ??= new RoleRepositoryBase(_dbContext, _transaction); }
        }

        public IRoleRepositoryV1 RoleRepositoryV1
        {
            get { return _roleRepositoryV1 ??= new RoleRepositoryV1(_dbContext, _transaction); }
        }

        #endregion

        #region RoleGroup Repository
        public IRoleGroupRepositoryBase RoleGroupRepositoryBase
        {
            get { return _roleGroupRepositoryBase ??= new RoleGroupRepositoryBase(_dbContext, _transaction); }
        }

        public IRoleGroupRepositoryV1 RoleGroupRepositoryV1
        {
            get { return _roleGroupRepositoryV1 ??= new RoleGroupRepositoryV1(_dbContext, _transaction); }
        }

        #endregion

        #region RolePermission Repository
        public IRolePermissionRepositoryBase RolePermissionRepositoryBase
        {
            get { return _rolePermissionRepositoryBase ??= new RolePermissionRepositoryBase(_dbContext, _transaction); }
        }

        public IRolePermissionRepositoryV1 RolePermissionRepositoryV1
        {
            get { return _rolePermissionRepositoryV1 ??= new RolePermissionRepositoryV1(_dbContext, _transaction); }
        }

        #endregion

        #endregion

        #region DB Settings
        #region SaveChanges
        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        #endregion

        #region Dispose

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                _dbContext.Dispose();
                _dbContext = null;
                disposedValue = true;
                //if (_transaction != null)
                //{
                //    _transaction.DisposeAsync();
                //}
                if (_efTransaction != null)
                {
                    _efTransaction.DisposeAsync();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        #endregion
        #endregion

        

        #region EF Transactions
        public async Task BeginEFTransactionAsync()
        {
            if (_efTransaction == null)
            {
                _efTransaction = await _dbContext.Database.BeginTransactionAsync();
            }
            else
            {
                ProblemReporter.ReportInternalServerError("Before opening a new transaction, you need to commit the previous one");
            }
        }

        public void CommitEFTrasnaction()
        {
            if (_efTransaction == null)
            {
                ProblemReporter.ReportInternalServerError("Cannot find transaction to commit");
            }

            _efTransaction.Commit();
        }

        public async Task RollBackEFTrasnactionAsync()
        {
            if (_efTransaction == null)
            {
                ProblemReporter.ReportInternalServerError("Cannot find transaction to rollback, or transaction alredy closed");
            }

            await _efTransaction.RollbackAsync();
        }

        #endregion

        #region Transactions
        protected NpgsqlTransaction _transaction;

        //public async Task<NpgsqlTransaction> BeginTransaction()
        //{
        //    if (_transaction == null)
        //    {
        //        NpgsqlConnection connection = new NpgsqlConnection(_dbContext.Database.GetConnectionString());
        //        connection.Open();

        //        _transaction = await connection.BeginTransactionAsync();
        //    }
        //    else
        //    {
        //        ProblemReporter.ReportInternalServerError("Before opening a new transaction, you need to commit the previous one");
        //    }
        //    return _transaction;
        //}

        //public async Task CommitTrasnactionAsync()
        //{
        //    if (_transaction == null)
        //    {
        //        ProblemReporter.ReportInternalServerError("Cannot find transaction to commit");
        //    }

        //    await _transaction.CommitAsync();
        //}

        //public async Task RollBackTrasnactionAsync()
        //{
        //    if (_transaction == null)
        //    {
        //        ProblemReporter.ReportInternalServerError("Cannot find transaction to rollback, or transaction alredy closed");
        //    }

        //    await _transaction.RollbackAsync();
        //}

        #endregion
    }
}
