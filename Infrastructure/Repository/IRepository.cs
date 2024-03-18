using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] navigationProperties);

        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] navigationProperties);

        TEntity GetOne(
            Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] navigationProperties);

        TEntity GetFirst(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] navigationProperties);

        TEntity GetById(object id);

        Task<IEnumerable<TEntity>> GetAllAsync(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] navigationProperties);

        Int64 GetCount(Expression<Func<TEntity, bool>> filter = null);
        Int64 GetSum(Expression<Func<TEntity, Int64>> selector, Expression<Func<TEntity, bool>> filter = null);
        Int64 GetMax(Expression<Func<TEntity, Int64>> selector, Expression<Func<TEntity, bool>> filter = null);
        Int64 GetMin(Expression<Func<TEntity, Int64>> selector, Expression<Func<TEntity, bool>> filter = null);
        bool GetExists(Expression<Func<TEntity, bool>> filter = null);

        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] navigationProperties);

        Task<TEntity> GetOneAsync(
            Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] navigationProperties);

        Task<TEntity> GetFirstAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] navigationProperties);

        Task<TEntity> GetByIdAsync(object id);

        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null);
        Task<int> GetSumAsync(Expression<Func<TEntity, int>> selector, Expression<Func<TEntity, bool>> filter = null);

        Task<bool> GetExistsAsync(Expression<Func<TEntity, bool>> filter = null);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entity);
    }
}
