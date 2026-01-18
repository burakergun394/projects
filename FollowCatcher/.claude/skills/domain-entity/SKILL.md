---
name: domain-entity
description: Create domain entities following DDD principles. Use when the user wants to create a new entity, add a domain model, implement domain logic, or mentions "entity", "domain", "model", "aggregate".
allowed-tools: Read, Grep, Glob, Write, Edit
---

# Domain Entity Creation Guide

**Project Requirements**: .NET 10, C# 14

## GUID Generation Strategy

**Important**: Use `Guid.CreateVersion7()` (available in .NET 9+, used in .NET 10) instead of `Guid.NewGuid()` for entity IDs.

### Benefits of Version 7 GUIDs
- **Chronological Ordering**: Naturally sorted by creation time
- **Better Database Performance**: Improved indexing and reduced page splits
- **Timestamp Embedded**: Contains creation timestamp (Unix epoch-based)
- **Collision Resistance**: Still globally unique like Version 4

```csharp
// ✅ Recommended (.NET 9+)
Id = Guid.CreateVersion7();

// ❌ Legacy approach
Id = Guid.NewGuid();
```

## Entity Structure

### Basic Entity Template

```csharp
// Location: Domain/{Feature}/{Feature}.cs
namespace {ProjectName}.Domain.{Features};

public class {Feature}
{
    public Guid Id { get; private set; }
    public string Property1 { get; private set; }
    public string Property2 { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    // Private constructor for EF Core
    private {Feature}() { }

    // Public constructor with validation
    public {Feature}(string property1, string property2)
    {
        Id = Guid.CreateVersion7(); // .NET 9+ timestamp-based GUID for better performance and ordering
        SetProperty1(property1);
        SetProperty2(property2);
        CreatedAt = DateTime.UtcNow;
    }

    // Behavior methods
    public void SetProperty1(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Property1 cannot be empty");

        Property1 = value;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetProperty2(string value)
    {
        Property2 = value;
        UpdatedAt = DateTime.UtcNow;
    }
}
```

### Entity with Value Objects

```csharp
public class Employee
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Email Email { get; private set; }  // Value Object
    public Address Address { get; private set; }  // Value Object
    public Location Location { get; private set; }  // Value Object

    private Employee() { }

    public Employee(string name, Email email, Location location)
    {
        Id = Guid.CreateVersion7(); // .NET 9+ timestamp-based GUID
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        Location = location;
    }
}
```

### Value Object Template

```csharp
// Location: Domain/{Feature}/{ValueObject}.cs
public record Location(double Latitude, double Longitude)
{
    public static Location Create(double latitude, double longitude)
    {
        if (latitude < -90 || latitude > 90)
            throw new DomainException("Invalid latitude");
        if (longitude < -180 || longitude > 180)
            throw new DomainException("Invalid longitude");

        return new Location(latitude, longitude);
    }
}

public record Email
{
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Email cannot be empty");
        if (!value.Contains("@"))
            throw new DomainException("Invalid email format");

        Value = value;
    }
}
```

### Entity with Navigation Properties

```csharp
public class Route
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Guid VehicleId { get; private set; }
    public Vehicle Vehicle { get; private set; }  // Navigation

    private readonly List<RouteStop> stops = new();
    public IReadOnlyCollection<RouteStop> Stops => stops.AsReadOnly();

    private Route() { }

    public Route(string name, Guid vehicleId)
    {
        Id = Guid.CreateVersion7(); // .NET 9+ timestamp-based GUID
        Name = name;
        VehicleId = vehicleId;
    }

    public void AddStop(Employee employee, int order)
    {
        var stop = new RouteStop(Id, employee.Id, order);
        stops.Add(stop);
    }

    public void RemoveStop(Guid employeeId)
    {
        var stop = stops.FirstOrDefault(s => s.EmployeeId == employeeId);
        if (stop != null)
            stops.Remove(stop);
    }
}
```

## EF Core Configuration

```csharp
// Location: Persistence/Configurations/{Feature}Configuration.cs
public class {Feature}Configuration : IEntityTypeConfiguration<{Feature}>
{
    public void Configure(EntityTypeBuilder<{Feature}> builder)
    {
        builder.ToTable("{Features}");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Property1)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Property2)
            .HasMaxLength(500);

        // Value Object configuration
        builder.OwnsOne(x => x.Location, location =>
        {
            location.Property(l => l.Latitude).HasColumnName("Latitude");
            location.Property(l => l.Longitude).HasColumnName("Longitude");
        });

        // Relationships
        builder.HasMany(x => x.Stops)
            .WithOne()
            .HasForeignKey(x => x.RouteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
```

## Domain Exception

```csharp
// Location: Domain/Exceptions/DomainException.cs
public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}
```

## DDD Principles

1. **Encapsulation**: Use private setters, expose behavior methods
2. **Validation**: Validate in constructors and setters
3. **Immutability**: Use records for Value Objects
4. **Rich Domain Model**: Put business logic in entities
5. **Aggregate Root**: Control access to child entities

## Checklist for New Entity

- [ ] Create entity class in Domain layer
- [ ] Add private parameterless constructor for EF
- [ ] Add public constructor with validation
- [ ] Add behavior methods instead of public setters
- [ ] Create Value Objects if needed
- [ ] Create EF Configuration
- [ ] Add DbSet to ApplicationDbContext
- [ ] Create migration
