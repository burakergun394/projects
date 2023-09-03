using Domain.Shared.Enums;

namespace Domain.Shared.Entities;

public abstract class AuditableEntity<TId> : Entity<TId>, IAuditableEntity
{
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string LastUpdatedBy { get; set; }
    public DateTime LastUpdatedAt { get; set; }

    protected AuditableEntity() { }

    protected AuditableEntity(TId id, Status status) : base(id, status)
    {
    }
}

public abstract class AuditableEntity : Entity, IAuditableEntity
{
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string LastUpdatedBy { get; set; }
    public DateTime LastUpdatedAt { get; set; }

    protected AuditableEntity() { }

    protected AuditableEntity(Status status) : base(status)
    {
    }
}

public interface IAuditableEntity
{
    string CreatedBy { get; set; }
    DateTime CreatedAt { get; set; }
    string LastUpdatedBy { get; set; }
    DateTime LastUpdatedAt { get; set; }
}