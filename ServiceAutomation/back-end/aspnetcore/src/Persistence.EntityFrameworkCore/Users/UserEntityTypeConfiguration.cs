using Domain.Products;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityFrameworkCore.Shared.EntityTypeConfiguratinos;

namespace Persistence.EntityFrameworkCore.Users;

internal class UserEntityTypeConfiguration : TenantAuditableEntityTypeConfiguration<User, Guid>
{
    public override void TenantAuditableEntityBuilder(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("USER", EntityFrameworkCoreConstants.DefaultSchemaName);

        builder.Property(x => x.Code)
            .HasColumnName("Code")
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(x => x.Surname)
            .HasColumnName("Surname")
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(x => x.Email)
            .HasColumnName("Email")
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(x => x.PasswordHash)
            .HasColumnName("PasswordHash")
            .HasColumnType("varbinary(1000)")
            .IsRequired();

        builder.Property(x => x.PasswordSalt)
            .HasColumnName("PasswordSalt")
            .HasColumnType("varbinary(100)")
            .IsRequired();

        builder.Property(x => x.Language)
            .HasColumnName("Language")
            .HasColumnType("varchar(20)")
            .HasConversion<string>()
            .IsRequired();
    }
}
