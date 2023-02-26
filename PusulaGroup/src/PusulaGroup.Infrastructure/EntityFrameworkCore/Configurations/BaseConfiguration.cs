using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PusulaGroup.Domain.Entities;

namespace PusulaGroup.Infrastructure.EntityFrameworkCore.Configurations
{
    public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
         
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("ID");

            Builder(builder);
        }

        public abstract void Builder(EntityTypeBuilder<TEntity> builder);
    }
}
