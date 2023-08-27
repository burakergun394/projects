using Domain.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityFrameworkCore.Shared.EntityTypeConfiguratinos;

internal abstract class AuditableEntityTypeConfiguration<T, TId> : EntityTypeConfiguration<T, TId>
    where T : AuditableEntity<TId>, new()
{
    public sealed override void EntityBuilder(EntityTypeBuilder<T> builder)
    {
        AuditableEntityBuilder(builder);

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

    public abstract void AuditableEntityBuilder(EntityTypeBuilder<T> builder);
}
