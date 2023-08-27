using Domain.Shared.Enums;

namespace Domain.Shared.Entities;

public abstract class Entity<TId>
{
    protected Entity() { }

    protected Entity(TId id, Status status)
    {
        Id = id;
    }

    public TId Id { get; protected set; }

    public Status Status { get; protected set; }
}