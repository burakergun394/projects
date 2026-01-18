using FollowCatcher.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace FollowCatcher.Persistence.Repositories;

public class Repository<T>(ApplicationDbContext dbContext) : IRepository<T> where T : Entity
{
    protected readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
    }

    public void Update(T entity)
    {
        _dbContext.Set<T>().Update(entity);
    }

    public void Remove(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }

    // Specification methods - NotImplemented for now
    public Task<IEnumerable<T>> FindAsync(Specification<T> specification, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public Task<T?> FindOneAsync(Specification<T> specification, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public Task<int> CountAsync(Specification<T> specification, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public Task<bool> AnyAsync(Specification<T> specification, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}
