using Domain.Identity.Roles;
using Domain.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityFrameworkCore.Shared.EntityTypeConfiguratinos;

namespace Persistence.EntityFrameworkCore.Identity.Roles;

internal class RoleEntityTypeConfiguration : TenantAuditableEntityTypeConfiguration<Role, Guid>
{
    public override void TenantAuditableEntityBuilder(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("ROLES", EntityFrameworkCoreConstants.DefaultSchemaName);

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(x => x.NormalizedName)
            .HasColumnName("NormalizedName")
            .HasColumnType("varchar(50)")
            .IsRequired();


        var admin = new Role(new Guid("A06310B2-5B34-4760-8B1B-33EC48DD3CFA"), Status.Active, "admin")
        {
            TenantCode = "SYSTEM",
            CreatedBy = "SYSTEM",
            CreatedAt = DateTime.Now,
            LastUpdatedBy = "",
            LastUpdatedAt = DateTime.MinValue
        };

        builder.HasData(admin);
    }
}