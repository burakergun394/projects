namespace FollowCatcher.Application.Common.Interfaces;

/// <summary>
/// Defines the application's database context interface.
/// This abstraction allows the Application layer to access the database
/// without depending on the concrete implementation in the Persistence layer.
/// </summary>
public interface IApplicationDbContext
{
    // Add entity set properties here as needed when you create domain entities
    // Example: IQueryable<Employee> Employees { get; }
    // Note: Use IQueryable instead of DbSet to avoid EF Core dependency

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
