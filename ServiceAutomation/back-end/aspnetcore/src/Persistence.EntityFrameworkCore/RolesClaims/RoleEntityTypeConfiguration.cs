using Domain.RolesClaims;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityFrameworkCore.Shared.EntityTypeConfiguratinos;

namespace Persistence.EntityFrameworkCore.RolesClaims;

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
    }
}