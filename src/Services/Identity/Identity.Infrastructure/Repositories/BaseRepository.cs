using Identity.Application.Contracts.Persistence;
using Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected IdentityDbContext _dbContext;
        public BaseRepository(IdentityDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        #region Base CRUD Operation

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            EntityEntry<T> dbEntityEntry = _dbContext.Entry(entity);

            _dbContext.Set<T>().Attach(entity);

            dbEntityEntry.State = EntityState.Deleted;
        }

        public void Delete(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _dbContext.Set<T>().Where(predicate).AsQueryable();
            foreach (T obj in query)
            {
                Delete(obj);
            }
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            IQueryable<T> set = _dbContext.Set<T>();

            for (int i = 0; i < includes.Length; i++)
            {
                set = set.Include(includes[i]);
            }

            if (predicate == null)
            {
                return set;
            }

            return set.Where(predicate);
        }

        public IQueryable<T> Get(params string[] includes)
        {
            IQueryable<T> set = _dbContext.Set<T>();

            for (int i = 0; i < includes.Length; i++)
            {
                set = set.Include(includes[i]);
            }

            return set;
        }

        public IQueryable<T> GetNoTracking(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            IQueryable<T> set = _dbContext.Set<T>();

            for (int i = 0; i < includes.Length; i++)
            {
                set = set.Include(includes[i]);
            }

            if (predicate == null)
            {
                return set;
            }

            return set.AsNoTracking().Where(predicate);
        }

        public IQueryable<T> GetNoTracking(params string[] includes)
        {
            IQueryable<T> set = _dbContext.Set<T>();

            for (int i = 0; i < includes.Length; i++)
            {
                set = set.Include(includes[i]);
            }

            return set.AsNoTracking();
        }

        public void Update(T entity, params Expression<Func<T, object>>[] updatedProperties)
        {
            var dbEntityEntry = _dbContext.Entry(entity);

            _dbContext.Set<T>().Attach(entity);

            if (updatedProperties != null && updatedProperties.Any())
            {
                foreach (var property in updatedProperties)
                {
                    dbEntityEntry.Property(property).IsModified = true;
                }
            }
            else
            {
                dbEntityEntry.State = EntityState.Modified;
            }
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> set = _dbContext.Set<T>();

            foreach (Expression<Func<T, object>> include in includes)
                set = set.Include(include);

            return set.FirstOrDefault(filter);
        }

        #endregion

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

    }
}
