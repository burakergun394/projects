using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonnelTransport.Domain.Employees;

namespace PersonnelTransport.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(100);

        builder.OwnsOne(x => x.HomeLocation, loc =>
        {
            loc.Property(l => l.Latitude).HasColumnName("HomeLatitude");
            loc.Property(l => l.Longitude).HasColumnName("HomeLongitude");
        });

        builder.OwnsOne(x => x.Shift, shift =>
        {
            shift.Property(s => s.StartTime).HasColumnName("ShiftStart");
            shift.Property(s => s.EndTime).HasColumnName("ShiftEnd");
        });

        builder.Property(x => x.SpecialNeeds)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            );
    }
}
