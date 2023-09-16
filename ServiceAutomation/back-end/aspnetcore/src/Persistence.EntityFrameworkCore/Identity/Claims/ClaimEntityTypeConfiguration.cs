using Domain.Identity.Claims;
using Domain.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityFrameworkCore.Shared.EntityTypeConfiguratinos;

namespace Persistence.EntityFrameworkCore.Identity.Claims;

internal class ClaimEntityTypeConfiguration : AuditableEntityTypeConfiguration<Claim, Guid>
{
    public override void AuditableEntityBuilder(EntityTypeBuilder<Claim> builder)
    {
        builder.ToTable("CLAIMS", EntityFrameworkCoreConstants.DefaultSchemaName);

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(x => x.NormalizedName)
            .HasColumnName("NormalizedName")
            .HasColumnType("varchar(50)")
            .IsRequired();

        var claimCreate = new Claim(new Guid("302B0514-4BD6-4D5F-9495-9BF129070423"), Status.Active, "claim.create")
        {
            CreatedBy = "SYSTEM",
            CreatedAt = DateTime.Now,
            LastUpdatedBy = "",
            LastUpdatedAt = DateTime.MinValue
        };
       
        var claimGet = new Claim(new Guid("6DF72AE0-ECB1-46CF-8208-41614D48E732"), Status.Active, "claim.get")
        {
            CreatedBy = "SYSTEM",
            CreatedAt = DateTime.Now,
            LastUpdatedBy = "",
            LastUpdatedAt = DateTime.MinValue
        };

        var systemRoleCreate = new Claim(new Guid("AF80BEB8-1E62-4A71-8791-A29EA9F728F0"), Status.Active, "system.role.create")
        {
            CreatedBy = "SYSTEM",
            CreatedAt = DateTime.Now,
            LastUpdatedBy = "",
            LastUpdatedAt = DateTime.MinValue
        };


        builder.HasData(claimCreate, claimGet, systemRoleCreate);
    }
}
