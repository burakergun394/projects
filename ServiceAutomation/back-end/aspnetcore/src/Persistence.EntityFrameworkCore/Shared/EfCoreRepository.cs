using Ardalis.GuardClauses;
using Domain.Shared;
using Domain.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Persistence.EntityFrameworkCore.Shared;

internal class EfCoreRepository<TEntity, TId, TContext> : IRepository<TEntity, TId>
    where TEntity : Entity<TId>, new()
    where TContext : DbContext, IUnitOfWork
{
    protected TContext Context;

    private DbSet<TEntity> dbSet;
    protected DbSet<TEntity> DbSet
    {
        get
        {
            return dbSet ??= Context.Set<TEntity>();
        }
    }

    public EfCoreRepository(TContext context)
    {
        Context = context;
    }

    public IUnitOfWork UnitOfWork => Context;

    public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
        return entity;
    }

    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        DbSet.Update(entity);
        return Task.FromResult(entity);
    }

    public Task<TEntity> DeleteAsync(TEntity entity)
    {
        DbSet.Remove(entity);
        return Task.FromResult(entity);
    }

    public async Task<TEntity> DeleteByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        Guard.Against.Null(entity);
        await DeleteAsync(entity);
        return entity;
    }

    public async Task<List<TEntity>> GetListAsync()
    {
        return await DbSet
            .ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await GetByPredicateAsync(x => x.Id.Equals(id), cancellationToken);
    }

    public async Task<TEntity> GetAsNoTrackingByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await GetAsNoTrackingByPredicateAsync(x => x.Id.Equals(id), cancellationToken);
    }

    protected async Task<List<TEntity>> GetListByPredicateAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, bool ignoreQueryFilter = false)
    {
        return ignoreQueryFilter
            ? await DbSet
                .IgnoreQueryFilters()
                .Where(predicate)
                .ToListAsync(cancellationToken)
            : await DbSet
                .Where(predicate)
                .ToListAsync(cancellationToken);
    }

    protected async Task<TEntity> GetByPredicateAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, bool ignoreQueryFilter = false)
    {
        return ignoreQueryFilter
            ? await DbSet
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(predicate, cancellationToken)
            : await DbSet
            .FirstOrDefaultAsync(predicate, cancellationToken);
    }

    protected async Task<TEntity> GetAsNoTrackingByPredicateAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, bool ignoreQueryFilter = false)
    {
        return ignoreQueryFilter
            ? await DbSet
                .AsNoTracking()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(predicate, cancellationToken)
            : await DbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(predicate, cancellationToken);
    }

    protected async Task<List<TType>> GetListOfSelectedColumnsByPredicateAsync<TType>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TType>> select, CancellationToken cancellationToken = default, bool ignoreQueryFilter = false)
        where TType : class
    {
        return ignoreQueryFilter
            ? await DbSet
                .IgnoreQueryFilters()
                .Where(predicate)
                .Select(select)
                .ToListAsync(cancellationToken)
            : await DbSet
                .Where(predicate)
                .Select(select)
                .ToListAsync(cancellationToken);
    }

    protected async Task<List<TType>> GetListOfSelectedColumnsAsync<TType>(Expression<Func<TEntity, TType>> select, CancellationToken cancellationToken = default, bool ignoreQueryFilter = false)
     where TType : class
    {
        return ignoreQueryFilter
            ? await DbSet
                .IgnoreQueryFilters()
                .Select(select)
                .ToListAsync(cancellationToken)
            : await DbSet
                .Select(select)
                .ToListAsync(cancellationToken);
    }

    protected async Task<TType> GetOfSelectedColumnsByPredicateAsync<TType>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TType>> select, CancellationToken cancellationToken = default, bool ignoreQueryFilter = false)
    {
        return ignoreQueryFilter
            ? await DbSet
                .IgnoreQueryFilters()
                .Where(predicate)
                .Select(select)
                .FirstOrDefaultAsync(cancellationToken)
            : await DbSet
                .Where(predicate)
                .Select(select)
                .FirstOrDefaultAsync(cancellationToken);
    }

    protected async Task<bool> AnyAsync(CancellationToken cancellationToken = default, bool ignoreQueryFilter = false)
    {
        return ignoreQueryFilter
            ? await DbSet
                 .IgnoreQueryFilters()
                 .AnyAsync(cancellationToken)
            : await DbSet
                 .AnyAsync(cancellationToken);
    }

    protected async Task<bool> AnyByPredicateAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, bool ignoreQueryFilter = false)
    {
        return ignoreQueryFilter
            ? await DbSet
                .IgnoreQueryFilters()
                .AnyAsync(predicate)
            : await DbSet
                .AnyAsync(predicate);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool isDisposing)
    {
        if (!isDisposing)
            return;

        Context.Dispose();
    }
}

internal class EfCoreRepository<TEntity, TContext> : IRepository<TEntity>
    where TEntity : Entity, new()
    where TContext : DbContext, IUnitOfWork
{
    protected TContext Context;

    private DbSet<TEntity> dbSet;
    protected DbSet<TEntity> DbSet
    {
        get
        {
            return dbSet ??= Context.Set<TEntity>();
        }
    }

    public EfCoreRepository(TContext context)
    {
        Context = context;
    }

    public IUnitOfWork UnitOfWork => Context;

    public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
        return entity;
    }

    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        DbSet.Update(entity);
        return Task.FromResult(entity);
    }

    public Task<TEntity> DeleteAsync(TEntity entity)
    {
        DbSet.Remove(entity);
        return Task.FromResult(entity);
    }

    public async Task<List<TEntity>> GetListAsync()
    {
        return await DbSet
            .ToListAsync();
    }

    protected async Task<List<TEntity>> GetListByPredicateAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, bool ignoreQueryFilter = false)
    {
        return ignoreQueryFilter
            ? await DbSet
                .IgnoreQueryFilters()
                .Where(predicate)
                .ToListAsync(cancellationToken)
            : await DbSet
                .Where(predicate)
                .ToListAsync(cancellationToken);
    }

    protected async Task<TEntity> GetByPredicateAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, bool ignoreQueryFilter = false)
    {
        return ignoreQueryFilter
            ? await DbSet
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(predicate, cancellationToken)
            : await DbSet
            .FirstOrDefaultAsync(predicate, cancellationToken);
    }

    protected async Task<TEntity> GetAsNoTrackingByPredicateAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, bool ignoreQueryFilter = false)
    {
        return ignoreQueryFilter
            ? await DbSet
                .AsNoTracking()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(predicate, cancellationToken)
            : await DbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(predicate, cancellationToken);
    }

    protected async Task<List<TType>> GetListOfSelectedColumnsByPredicateAsync<TType>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TType>> select, CancellationToken cancellationToken = default, bool ignoreQueryFilter = false)
        where TType : class
    {
        return ignoreQueryFilter
            ? await DbSet
                .IgnoreQueryFilters()
                .Where(predicate)
                .Select(select)
                .ToListAsync(cancellationToken)
            : await DbSet
                .Where(predicate)
                .Select(select)
                .ToListAsync(cancellationToken);
    }

    protected async Task<List<TType>> GetListOfSelectedColumnsAsync<TType>(Expression<Func<TEntity, TType>> select, CancellationToken cancellationToken = default, bool ignoreQueryFilter = false)
        where TType : class
    {
        return ignoreQueryFilter
            ? await DbSet
                .IgnoreQueryFilters()
                .Select(select)
                .ToListAsync(cancellationToken)
            : await DbSet
                .Select(select)
                .ToListAsync(cancellationToken);
    }

    protected async Task<TType> GetOfSelectedColumnsByPredicateAsync<TType>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TType>> select, CancellationToken cancellationToken = default, bool ignoreQueryFilter = false)
    {
        return ignoreQueryFilter
            ? await DbSet
                .IgnoreQueryFilters()
                .Where(predicate)
                .Select(select)
                .FirstOrDefaultAsync(cancellationToken)
            : await DbSet
                .Where(predicate)
                .Select(select)
                .FirstOrDefaultAsync(cancellationToken);
    }

    protected async Task<bool> AnyAsync(CancellationToken cancellationToken = default, bool ignoreQueryFilter = false)
    {
        return ignoreQueryFilter
            ? await DbSet
                 .IgnoreQueryFilters()
                 .AnyAsync(cancellationToken)
            : await DbSet
                 .AnyAsync(cancellationToken);
    }

    protected async Task<bool> AnyByPredicateAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, bool ignoreQueryFilter = false)
    {
        return ignoreQueryFilter
            ? await DbSet
                .IgnoreQueryFilters()
                .AnyAsync(predicate)
            : await DbSet
                .AnyAsync(predicate);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool isDisposing)
    {
        if (!isDisposing)
            return;

        Context.Dispose();
    }
}