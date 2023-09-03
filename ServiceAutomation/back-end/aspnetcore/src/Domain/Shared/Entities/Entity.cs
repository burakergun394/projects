using Domain.Shared.Enums;

namespace Domain.Shared.Entities;

public abstract class Entity<TId>
{
    protected Entity() { }

    protected Entity(TId id, Status status)
    {
        Id = id;
        Status = status;
    }

    public TId Id { get; protected set; }

    public Status Status { get; protected set; }
}

public abstract class Entity
{
    protected Entity() { }

    protected Entity(Status status)
    {
        Status = status;
    }

    public Status Status { get; protected set; }
}