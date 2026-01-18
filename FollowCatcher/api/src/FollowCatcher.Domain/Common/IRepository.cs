namespace FollowCatcher.Domain.Common;


public interface IRepository<T> where T : Entity
{

    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);


    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);


    Task<IEnumerable<T>> FindAsync(Specification<T> specification, CancellationToken cancellationToken = default);


    Task<T?> FindOneAsync(Specification<T> specification, CancellationToken cancellationToken = default);


    Task<int> CountAsync(Specification<T> specification, CancellationToken cancellationToken = default);


    Task AddAsync(T entity, CancellationToken cancellationToken = default);


    void Update(T entity);


    void Remove(T entity);


    Task<bool> AnyAsync(Specification<T> specification, CancellationToken cancellationToken = default);
}
