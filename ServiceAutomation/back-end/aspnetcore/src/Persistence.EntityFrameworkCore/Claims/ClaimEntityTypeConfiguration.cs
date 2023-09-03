using Domain.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityFrameworkCore.Shared.EntityTypeConfiguratinos;

namespace Persistence.EntityFrameworkCore.Claims;

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
    }
}
