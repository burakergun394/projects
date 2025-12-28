using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonnelTransport.Domain.Vehicles;

namespace PersonnelTransport.Persistence.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.PlateNumber).IsRequired().HasMaxLength(20);
        builder.HasIndex(x => x.PlateNumber).IsUnique();

        builder.OwnsOne(x => x.DepotLocation, loc =>
        {
            loc.Property(l => l.Latitude).HasColumnName("DepotLatitude");
            loc.Property(l => l.Longitude).HasColumnName("DepotLongitude");
        });

        builder.Property(x => x.Type).HasConversion<string>();
    }
}
