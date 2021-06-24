using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Contracts.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
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

        Task CommitTrasnactionAsync();

        Task RollBackTrasnactionAsync();
        #endregion
    }
}
