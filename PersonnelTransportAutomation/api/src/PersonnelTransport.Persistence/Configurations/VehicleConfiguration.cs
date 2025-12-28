using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonnelTransport.Domain.Vehicles;

namespace PersonnelTransport.Persistence.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        // Seed Data: 5 Vehicles
        // Use a fixed base time to ensure deterministic GUIDs (so migrations are stable)
        var baseTime = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);
        
        var vehicles = new List<object>();
        var depotLocations = new List<object>();

        for (int i = 0; i < 5; i++)
        {
            // Create deterministic time-based GUID
            var id = Guid.CreateVersion7(baseTime.AddMinutes(i));
            
            vehicles.Add(new
            {
                Id = id,
                PlateNumber = $"34 ABC {100 + i}",
                Capacity = 15 + (i * 2), // Varying capacity
                Type = VehicleType.Bus, // Enum
                CostPerKm = 5.0 + (i * 0.5)
            });

            depotLocations.Add(new
            {
                VehicleId = id,
                Latitude = 41.0000 + (i * 0.01),
                Longitude = 29.0000 + (i * 0.01)
            });
        }

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PlateNumber).IsRequired().HasMaxLength(20);
        builder.HasIndex(x => x.PlateNumber).IsUnique();

        builder.OwnsOne(x => x.DepotLocation, loc =>
        {
            loc.Property(l => l.Latitude).HasColumnName("DepotLatitude");
            loc.Property(l => l.Longitude).HasColumnName("DepotLongitude");
            loc.HasData(depotLocations);
        });

        builder.Property(x => x.Type).HasConversion<string>();

        builder.HasData(vehicles);
    }
}
