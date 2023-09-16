using Domain.Identity.Users;
using Domain.Shared.Enums;
using Domain.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityFrameworkCore.Shared.EntityTypeConfiguratinos;

namespace Persistence.EntityFrameworkCore.Identity.Users;

internal class UserEntityTypeConfiguration : TenantAuditableEntityTypeConfiguration<User, Guid>
{
    public override void TenantAuditableEntityBuilder(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("USERS", EntityFrameworkCoreConstants.DefaultSchemaName);

        builder.Property(x => x.Username)
            .HasColumnName("Username")
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(x => x.NormalizedUsername)
            .HasColumnName("NormalizedUsername")
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(x => x.RoleId)
           .HasColumnName("RoleId")
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

        PasswordHasherHelper.HashAndSaltPassword("Password12*", out byte[] hash, out byte[] salt);
        var systemUser = new User(new Guid("40EB7D2F-5969-4AFA-90A4-84AA0377FC04"),
                                  Status.Active,
                                  "SystemUser",
                                  new Guid("A06310B2-5B34-4760-8B1B-33EC48DD3CFA"),
                                  "System",
                                  "System",
                                  "system@gmail.com",
                                  hash,
                                  salt)
        {
            TenantCode = "SYSTEM",
            CreatedBy = "SYSTEM",
            CreatedAt = DateTime.Now,
            LastUpdatedBy = "",
            LastUpdatedAt = DateTime.MinValue
        };

        builder.HasData(systemUser);
    }
}
