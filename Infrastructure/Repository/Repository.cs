using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal DbContext Context { get; }

        internal DbSet<TEntity> DbSet { get; }

        public Repository(DbContext context) : base()
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null, int? take = null, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return GetQueryable(null, orderBy, skip, take, navigationProperties).AsParallel().ToList();
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int? skip = null, int? take = null,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return GetQueryable(filter, orderBy, skip, take, navigationProperties).ToList();
        }

        public virtual TEntity GetOne(Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return GetQueryable(filter, null, null, null, navigationProperties).SingleOrDefault();
        }

        public virtual TEntity GetFirst(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return GetQueryable(filter, orderBy, null, null, navigationProperties).FirstOrDefault();
        }

        public virtual TEntity GetById(object id)
        {
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return DbSet.Find(id);
        }

        public virtual Int64 GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).Count();
        }

        public virtual Int64 GetSum(Expression<Func<TEntity, Int64>> selector, Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).Sum(selector);
        }

        public virtual Int64 GetMax(Expression<Func<TEntity, Int64>> selector, Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).Max(selector);
        }

        public virtual Int64 GetMin(Expression<Func<TEntity, Int64>> selector, Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).Min(selector);
        }

        public virtual bool GetExists(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).Any();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int? skip = null, int? take = null,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return await GetQueryable(null, orderBy, skip, take, navigationProperties)
                .ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int? skip = null, int? take = null,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return await GetQueryable(filter, orderBy, skip, take, navigationProperties).ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return await GetQueryable(filter, null, null, null, navigationProperties).SingleOrDefaultAsync();
        }

        public virtual async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return await GetQueryable(filter, orderBy, null, null, navigationProperties).FirstOrDefaultAsync();
        }

        public virtual Task<TEntity> GetByIdAsync(object id)
        {
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return DbSet.FindAsync(id).AsTask();
        }

        public virtual Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).CountAsync();
        }

        public virtual Task<int> GetSumAsync(Expression<Func<TEntity, int>> selector, Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).SumAsync(selector);
        }

        public virtual Task<bool> GetExistsAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).AnyAsync();
        }

        public virtual void Create(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            //Context.Set<TEntity>().Attach(entity);
            //Context.Entry(entity).State = EntityState.Modified;

            DbSet.Update(entity);

        }

        public virtual void Delete(object id)
        {
            TEntity entity = DbSet.Find(id);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            DbSet.Remove(entity);
        }

        private IQueryable<TEntity> GetQueryable(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();


            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = navigationProperties
                .Aggregate(query, (current, navigationProperty) => current.Include(navigationProperty));

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query
                .AsQueryable()
                .AsNoTracking();
        }
    }
}
