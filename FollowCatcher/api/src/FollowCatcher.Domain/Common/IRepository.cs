namespace FollowCatcher.Domain.Common;

/// <summary>
/// Base repository interface for generic CRUD operations.
/// </summary>
/// <typeparam name="T">The entity type.</typeparam>
public interface IRepository<T> where T : Entity
{
    /// <summary>
    /// Gets an entity by its identifier.
    /// </summary>
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all entities.
    /// </summary>
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds entities matching the given specification.
    /// </summary>
    Task<IEnumerable<T>> FindAsync(Specification<T> specification, CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds a single entity matching the given specification.
    /// </summary>
    Task<T?> FindOneAsync(Specification<T> specification, CancellationToken cancellationToken = default);

    /// <summary>
    /// Counts entities matching the given specification.
    /// </summary>
    Task<int> CountAsync(Specification<T> specification, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new entity.
    /// </summary>
    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    void Update(T entity);

    /// <summary>
    /// Removes an entity.
    /// </summary>
    void Remove(T entity);

    /// <summary>
    /// Checks if any entity exists matching the given specification.
    /// </summary>
    Task<bool> AnyAsync(Specification<T> specification, CancellationToken cancellationToken = default);
}
