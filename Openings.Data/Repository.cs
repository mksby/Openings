using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Openings.Data
{
    public sealed class Repository<T> : BaseRepository<T>, IRepository<T> where T : class
    {
        public Repository(DbContext context) : base(context)
        {
        }

        #region Get Functions

        public T SingleOrDefault(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool enableTracking = true,
            bool ignoreQueryFilters = false)
        {
            IQueryable<T> query = _dbSet;

            if (!enableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (ignoreQueryFilters) query = query.IgnoreQueryFilters();

            return orderBy != null ? orderBy(query).FirstOrDefault() : query.FirstOrDefault();
        }

        #endregion

        public void Dispose()
        {
            _dbContext?.Dispose();
        }


        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Delete(params T[] entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Delete(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        #region Insert Functions

        public T Insert(T entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        public void Insert(params T[] entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Insert(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public T InsertNotExists(Expression<Func<T, bool>> predicate, T entity)
        {
            if (_dbSet.Any(predicate)) return _dbSet.SingleOrDefault(predicate.Compile());
            _dbSet.Add(entity);
            return entity;

        }
        

        #endregion


        #region Update Functions

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Update(params T[] entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public void Update(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        #endregion
    }
}