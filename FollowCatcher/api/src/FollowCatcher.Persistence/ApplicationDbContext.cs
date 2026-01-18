using System.Reflection;
using FollowCatcher.Application.Common.Interfaces;
using FollowCatcher.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FollowCatcher.Persistence;

/// <summary>
/// Application database context implementing both IApplicationDbContext and IUnitOfWork.
/// This class is responsible for database access and transaction management.
/// </summary>
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext, IUnitOfWork
{
    private IDbContextTransaction? _currentTransaction;

    // Add DbSet properties for your entities here
    // Example: public DbSet<Employee> Employees => Set<Employee>();

    /// <summary>
    /// Configures the model that was discovered by convention from the entity types.
    /// Applies all entity configurations from the assembly.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply all entity configurations from the assembly
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// Publishes domain events before saving.
    /// </summary>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Update timestamps for tracked entities
        UpdateTimestamps();

        // Publish domain events before saving
        await PublishDomainEventsAsync(cancellationToken);

        return await base.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Begins a new database transaction.
    /// </summary>
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction != null)
        {
            throw new InvalidOperationException("A transaction is already in progress.");
        }

        _currentTransaction = await Database.BeginTransactionAsync(cancellationToken);
    }

    /// <summary>
    /// Commits the current database transaction.
    /// </summary>
    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction == null)
        {
            throw new InvalidOperationException("No transaction is in progress.");
        }

        try
        {
            await SaveChangesAsync(cancellationToken);
            await _currentTransaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            _currentTransaction?.Dispose();
            _currentTransaction = null;
        }
    }

    /// <summary>
    /// Rolls back the current database transaction.
    /// </summary>
    public async Task RollbackTransactionAsync()
    {
        if (_currentTransaction == null)
        {
            return;
        }

        try
        {
            await _currentTransaction.RollbackAsync();
        }
        finally
        {
            _currentTransaction?.Dispose();
            _currentTransaction = null;
        }
    }

    /// <summary>
    /// Updates CreatedAt and UpdatedAt timestamps for tracked entities.
    /// </summary>
    private void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries<Entity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                // CreatedAt is set in the Entity constructor, so we don't need to set it here
            }
            else if (entry.State == EntityState.Modified)
            {
                // Use reflection to call MarkAsUpdated (it's protected)
                var method = typeof(Entity).GetMethod("MarkAsUpdated",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                method?.Invoke(entry.Entity, null);
            }
        }
    }

    /// <summary>
    /// Publishes domain events from tracked entities.
    /// </summary>
    private async Task PublishDomainEventsAsync(CancellationToken cancellationToken)
    {
        var domainEvents = ChangeTracker.Entries<Entity>()
            .Select(x => x.Entity)
            .Where(x => x.DomainEvents.Any())
            .SelectMany(x => x.DomainEvents)
            .ToList();

        var entities = ChangeTracker.Entries<Entity>()
            .Where(x => x.Entity.DomainEvents.Any())
            .Select(x => x.Entity)
            .ToList();

        // Clear domain events from entities
        entities.ForEach(entity => entity.ClearDomainEvents());

        // TODO: Publish domain events using MediatR or your event dispatcher
        // foreach (var domainEvent in domainEvents)
        // {
        //     await _mediator.Publish(domainEvent, cancellationToken);
        // }

        await Task.CompletedTask;
    }
}
