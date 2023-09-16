using Domain.Identity.Tenants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityFrameworkCore.Shared.EntityTypeConfiguratinos;

namespace Persistence.EntityFrameworkCore.Identity.Tenants;

internal class TenantEntityTypeConfiguration : AuditableEntityTypeConfiguration<Tenant>
{
    public override void AuditableEntityBuilder(EntityTypeBuilder<Tenant> builder)
    {
        builder.ToTable("TENANTS", EntityFrameworkCoreConstants.DefaultSchemaName);

        builder.HasKey(x => x.TenantCode);

        var _default = Tenant.Create("SYSTEM");
        _default.CreatedBy = "SYSTEM";
        _default.CreatedAt = DateTime.UtcNow;
        _default.LastUpdatedBy = "";
        _default.LastUpdatedAt = default;

        var aytas = Tenant.Create("AYTAS");
        aytas.CreatedBy = "SYSTEM";
        aytas.CreatedAt = DateTime.UtcNow;
        aytas.LastUpdatedBy = "";
        aytas.LastUpdatedAt = default;

        var data = new List<Tenant> { _default, aytas };

        builder.HasData(data);
    }
}