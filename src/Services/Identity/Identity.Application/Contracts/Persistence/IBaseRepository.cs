using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Contracts.Persistence
{
    public interface IBaseRepository<T> where T : class
    {
        T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes);

        IQueryable<T> Get(Expression<Func<T, bool>> predicate, params string[] includes);
        IQueryable<T> Get(params string[] includes);
        IQueryable<T> GetNoTracking(Expression<Func<T, bool>> predicate, params string[] includes);
        IQueryable<T> GetNoTracking(params string[] includes);
        void Add(T entity);

        void Update(T entity, params Expression<Func<T, object>>[] updatedProperties);
        void Delete(T entity);

        void Delete(Expression<Func<T, bool>> predicate);
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}

//Scaffold - DbContext "Server=34.107.123.66; Port=5432; Database=Authorization; User Id=api_user; Password=vDsweiobeqo9ixOA" Npgsql.EntityFrameworkCore.PostgreSQL - o Entities - ContextDir DBContext - Context Persisence - ContextNamespace Identity.Infrastructure.Persistence
