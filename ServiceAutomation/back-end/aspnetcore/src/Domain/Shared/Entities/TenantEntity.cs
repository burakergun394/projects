using Domain.Shared.Enums;

namespace Domain.Shared.Entities;

public abstract class TenantEntity<TId> : Entity<TId>
{
    protected TenantEntity() { }

    protected TenantEntity(TId id, Status status) : base(id, status)
    {
    }

    public string TenantCode { get; set; }
}
