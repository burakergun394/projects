using Domain.Identity.RolesClaims;
using Domain.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityFrameworkCore.Shared.EntityTypeConfiguratinos;

namespace Persistence.EntityFrameworkCore.Identity.RolesClaims;

internal class RoleClaimEntityTypeConfiguration : AuditableEntityTypeConfiguration<RoleClaim>
{
    public override void AuditableEntityBuilder(EntityTypeBuilder<RoleClaim> builder)
    {
        builder.ToTable("ROLES_CLAIMS", EntityFrameworkCoreConstants.DefaultSchemaName);

        builder.HasKey(x => new { x.RoleId, x.ClaimId });

        builder.Property(x => x.RoleId)
            .HasColumnName("RoleId")
            .IsRequired();

        builder.Property(x => x.ClaimId)
            .HasColumnName("ClaimId")
            .IsRequired();

        var claimCreateToAdmin = new RoleClaim(Status.Active, new Guid("A06310B2-5B34-4760-8B1B-33EC48DD3CFA"), new Guid("302B0514-4BD6-4D5F-9495-9BF129070423"))
        {
            CreatedBy = "SYSTEM",
            CreatedAt = DateTime.Now,
            LastUpdatedBy = "",
            LastUpdatedAt = DateTime.MinValue
        };

        var claimGetToAdmin = new RoleClaim(Status.Active, new Guid("A06310B2-5B34-4760-8B1B-33EC48DD3CFA"), new Guid("6DF72AE0-ECB1-46CF-8208-41614D48E732"))
        {
            CreatedBy = "SYSTEM",
            CreatedAt = DateTime.Now,
            LastUpdatedBy = "",
            LastUpdatedAt = DateTime.MinValue
        };

        var sytemRoleCreateToAdmin = new RoleClaim(Status.Active, new Guid("A06310B2-5B34-4760-8B1B-33EC48DD3CFA"), new Guid("AF80BEB8-1E62-4A71-8791-A29EA9F728F0"))
        {
            CreatedBy = "SYSTEM",
            CreatedAt = DateTime.Now,
            LastUpdatedBy = "",
            LastUpdatedAt = DateTime.MinValue
        };

        builder.HasData(claimCreateToAdmin, claimGetToAdmin, sytemRoleCreateToAdmin);
    }
}