using FollowCatcher.Domain.Instagram;

namespace FollowCatcher.Domain.Data; 

public interface IApplicationDbContext
{
    // Add entity set properties here as needed when you create domain entities
    // Example: IQueryable<Employee> Employees { get; }
    // Note: Use IQueryable instead of DbSet to avoid EF Core dependency
    IQueryable<InstagramTrackedAccount> InstagramTrackedAccounts { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
