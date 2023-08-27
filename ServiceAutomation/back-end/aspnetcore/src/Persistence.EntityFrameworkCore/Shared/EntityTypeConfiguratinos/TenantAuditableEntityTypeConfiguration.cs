using Domain.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityFrameworkCore.Shared.EntityTypeConfiguratinos;

internal abstract class TenantAuditableEntityTypeConfiguration<T, TId> : TenantEntityTypeConfiguration<T, TId>
    where T : TenantAuditableEntity<TId>, new()
{
    public sealed override void TenantEntityBuilder(EntityTypeBuilder<T> builder)
    {
        TenantAuditableEntityBuilder(builder);

        builder.Property(x => x.CreatedBy)
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnType("datetime2(7)")
            .IsRequired();

        builder.Property(x => x.LastUpdatedBy)
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(x => x.LastUpdatedAt)
            .HasColumnType("datetime2(7)")
            .IsRequired();
    }

    public abstract void TenantAuditableEntityBuilder(EntityTypeBuilder<T> builder);
}
