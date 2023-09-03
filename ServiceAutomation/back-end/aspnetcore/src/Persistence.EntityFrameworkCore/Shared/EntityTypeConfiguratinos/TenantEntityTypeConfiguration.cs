using Domain.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityFrameworkCore.Shared.EntityTypeConfiguratinos;

internal abstract class TenantEntityTypeConfiguration<T, TId> : EntityTypeConfiguration<T, TId>
    where T : TenantEntity<TId>, new()
{
    public sealed override void EntityBuilder(EntityTypeBuilder<T> builder)
    {
        builder.Property(x => x.TenantCode)
            .HasColumnOrder(1)
            .HasColumnType("varchar(50)")
            .IsRequired();

        TenantEntityBuilder(builder);
    }

    public abstract void TenantEntityBuilder(EntityTypeBuilder<T> builder);
}
