using Domain.Shared.Entities;
using Domain.Shared.Enums;
namespace Domain.Identity.Tenants;

public class Tenant : AuditableEntity
{
    public string TenantCode { get; private set; }

    public Tenant()
    {
    }

    public Tenant(Status status, string tenantCode) : base(status)
    {
        TenantCode = tenantCode;
    }

    public static Tenant Create(string tenantCode)
    {
        return new(Status.Active, tenantCode);
    }
}