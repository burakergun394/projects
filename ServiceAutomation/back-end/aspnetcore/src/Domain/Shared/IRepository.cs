using Domain.Shared.Entities;

namespace Domain.Shared;

public interface IRepository<TEntity, TId> : IDisposable where TEntity : Entity<TId>
{
    IUnitOfWork UnitOfWork { get; }
    Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> DeleteAsync(TEntity entity);
    Task<TEntity> DeleteByIdAsync(TId id, CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetListAsync();
    Task<TEntity> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
    Task<TEntity> GetAsNoTrackingByIdAsync(TId id, CancellationToken cancellationToken = default);
}
