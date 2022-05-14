using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Openings.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class;
        IRepositoryReadOnly<TEntity> GetReadOnlyRepository<TEntity>() where TEntity : class;
        IRepositoryReadOnlyAsync<TEntity> GetReadOnlyRepositoryAsync<TEntity>() where TEntity : class;
        IDeleteRepository<TEntity> DeleteRepository<TEntity>() where TEntity : class;

        int Commit(bool autoHistory = false);
        Task<int> CommitAsync(bool autoHistory = false);
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext Context { get; }
    }
}