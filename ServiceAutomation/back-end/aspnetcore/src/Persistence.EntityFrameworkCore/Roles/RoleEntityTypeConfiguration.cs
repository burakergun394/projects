using Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityFrameworkCore.Shared.EntityTypeConfiguratinos;

namespace Persistence.EntityFrameworkCore.Roles;

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
    }
}