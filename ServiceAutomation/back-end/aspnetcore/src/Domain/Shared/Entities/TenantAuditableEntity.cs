using Domain.Shared.Enums;

namespace Domain.Shared.Entities;

public abstract class TenantAuditableEntity<TId> : TenantEntity<TId>, IAuditableEntity
{
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string LastUpdatedBy { get; set; }
    public DateTime LastUpdatedAt { get; set; }

    protected TenantAuditableEntity() { }

    protected TenantAuditableEntity(TId id, Status status) : base(id, status)
    {
      
    }
}