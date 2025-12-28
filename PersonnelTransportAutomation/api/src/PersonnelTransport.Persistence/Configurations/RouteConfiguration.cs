namespace PersonnelTransport.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonnelTransport.Domain.Routes;

public class RouteConfiguration : IEntityTypeConfiguration<Route>
{
    public void Configure(EntityTypeBuilder<Route> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Status).HasConversion<string>();

        builder.OwnsMany(x => x.Stops, stop =>
        {
            stop.WithOwner().HasForeignKey("RouteId");
            stop.HasKey(x => x.Id);

            stop.Property(x => x.Order).IsRequired();
            stop.OwnsOne(x => x.Location, loc =>
            {
                loc.Property(l => l.Latitude).HasColumnName("Latitude");
                loc.Property(l => l.Longitude).HasColumnName("Longitude");
            });

            // Storing GUID list as comma-separated string for MVP simplicity (PostgreSQL array could also be used but this is more portable for now)
            stop.Property(x => x.PickupPersonnelIds)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(Guid.Parse).ToList()
                );
        });
    }
}
