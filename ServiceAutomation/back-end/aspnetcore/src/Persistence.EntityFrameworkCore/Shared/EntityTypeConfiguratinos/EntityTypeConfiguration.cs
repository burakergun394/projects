using Domain.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityFrameworkCore.Shared.EntityTypeConfiguratinos;

internal abstract class EntityTypeConfiguration<T, TId> : IEntityTypeConfiguration<T>
    where T : Entity<TId>, new()
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Status)
            .HasColumnName("Status")
            .HasColumnType("varchar(10)")
            .HasConversion<string>();

        EntityBuilder(builder);
    }

    public abstract void EntityBuilder(EntityTypeBuilder<T> builder);
}

internal abstract class EntityTypeConfiguration<T> : IEntityTypeConfiguration<T>
    where T : Entity, new()
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(x => x.Status)
            .HasColumnName("Status")
            .HasColumnType("varchar(10)")
            .HasConversion<string>();

        EntityBuilder(builder);
    }

    public abstract void EntityBuilder(EntityTypeBuilder<T> builder);
}