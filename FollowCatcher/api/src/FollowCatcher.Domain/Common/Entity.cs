namespace FollowCatcher.Domain.Common;

/// <summary>
/// Base entity class with domain events support and common properties.
/// </summary>
public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = [];

    /// <summary>
    /// Unique identifier for the entity.
    /// </summary>
    public Guid Id { get; protected set; } = Guid.NewGuid();

    /// <summary>
    /// Timestamp when the entity was created.
    /// </summary>
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;

    /// <summary>
    /// Timestamp when the entity was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; protected set; }

    /// <summary>
    /// Timestamp when the entity was soft deleted. Null if not deleted.
    /// </summary>
    public DateTime? DeletedAt { get; protected set; }

    /// <summary>
    /// Read-only collection of domain events associated with this entity.
    /// </summary>
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Adds a domain event to the entity.
    /// </summary>
    /// <param name="domainEvent">The domain event to add.</param>
    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    /// <summary>
    /// Removes a specific domain event from the entity.
    /// </summary>
    /// <param name="domainEvent">The domain event to remove.</param>
    protected void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    /// <summary>
    /// Clears all domain events from the entity.
    /// </summary>
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    /// <summary>
    /// Updates the UpdatedAt timestamp to the current UTC time.
    /// </summary>
    protected void MarkAsUpdated()
    {
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Soft deletes the entity by setting the DeletedAt timestamp.
    /// </summary>
    public void MarkAsDeleted()
    {
        DeletedAt = DateTime.UtcNow;
        MarkAsUpdated();
    }

    /// <summary>
    /// Restores a soft-deleted entity by clearing the DeletedAt timestamp.
    /// </summary>
    public void Restore()
    {
        DeletedAt = null;
        MarkAsUpdated();
    }

    /// <summary>
    /// Checks if the entity is soft-deleted.
    /// </summary>
    public bool IsDeleted => DeletedAt.HasValue;
}
