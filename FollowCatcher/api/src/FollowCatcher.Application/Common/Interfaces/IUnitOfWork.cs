namespace FollowCatcher.Application.Common.Interfaces;

/// <summary>
/// Defines the Unit of Work pattern for managing database transactions.
/// The Unit of Work maintains a list of objects affected by a business transaction
/// and coordinates writing changes to the database.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Saves all changes made in this unit of work to the database.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Begins a new database transaction.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Commits the current database transaction.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Rolls back the current database transaction.
    /// </summary>
    Task RollbackTransactionAsync();
}
