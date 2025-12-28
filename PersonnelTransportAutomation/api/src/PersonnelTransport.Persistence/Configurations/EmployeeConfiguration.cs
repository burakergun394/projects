using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonnelTransport.Domain.Employees;

namespace PersonnelTransport.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        // Seed Data: 100 Employees
        // Use a fixed base time to ensure deterministic GUIDs
        var baseTime = new DateTimeOffset(2024, 1, 2, 0, 0, 0, TimeSpan.Zero);
        
        var employees = new List<object>();
        var homeLocations = new List<object>();
        var shifts = new List<object>();


        for (int i = 0; i < 100; i++)
        {
           var id = Guid.CreateVersion7(baseTime.AddMinutes(i));
            employees.Add(new
            {
                Id = id,
                FirstName = $"Employee{i + 1}",
                LastName = $"Surname{i + 1}",
                SpecialNeeds = i % 10 == 0 ? new List<string> { "Wheelchair" } : new List<string>() // 10% have special needs
            });

            homeLocations.Add(new
            {
                EmployeeId = id,
                Latitude = 41.0000 + (i * 0.005),  // Deterministic: scatter around Istanbul
                Longitude = 29.0000 + (i * 0.005)
            });

            shifts.Add(new
            {
                EmployeeId = id,
                StartTime = TimeSpan.FromHours(8 + (i % 3)), // Staggered shifts 08:00, 09:00, 10:00
                EndTime = TimeSpan.FromHours(17 + (i % 3))
            });
        }


        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(100);

        builder.OwnsOne(x => x.HomeLocation, loc =>
        {
            loc.Property(l => l.Latitude).HasColumnName("HomeLatitude");
            loc.Property(l => l.Longitude).HasColumnName("HomeLongitude");
            loc.HasData(homeLocations);
        });

        builder.OwnsOne(x => x.Shift, shift =>
        {
            shift.Property(s => s.StartTime).HasColumnName("ShiftStart");
            shift.Property(s => s.EndTime).HasColumnName("ShiftEnd");
            shift.HasData(shifts);
        });

        builder.Property(x => x.SpecialNeeds)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            );

        builder.HasData(employees);
    }
}
