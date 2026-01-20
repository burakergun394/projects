namespace FollowCatcher.Domain.Common;


public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = [];


    public Guid Id { get; protected set; } = Guid.CreateVersion7();


    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;


    public DateTime? UpdatedAt { get; protected set; }


    public DateTime? DeletedAt { get; protected set; }


    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();


    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }


    protected void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }


    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }


    protected void MarkAsUpdated()
    {
        UpdatedAt = DateTime.UtcNow;
    }


    public void MarkAsDeleted()
    {
        DeletedAt = DateTime.UtcNow;
        MarkAsUpdated();
    }


    public void Restore()
    {
        DeletedAt = null;
        MarkAsUpdated();
    }


    public bool IsDeleted => DeletedAt.HasValue;
}
