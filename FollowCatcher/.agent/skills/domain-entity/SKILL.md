---
name: domain-entity
description: Create domain entities following DDD principles. Use when the user wants to create a new entity, add a domain model, implement domain logic, or mentions "entity", "domain", "model", "aggregate".
allowed-tools: Read, Grep, Glob, Write, Edit
---

# Domain Entity Creation Guide

> **Note**: This skill focuses on the *execution steps* ("The Recipe"). For theoretical concepts (DDD, Aggregates, Domain Events), refer to **[.agent/skills/clean-architecture/SKILL.md](file:///c:/Users/Burak/Documents/MY/GITHUB/BURAKERGUN394/projects/FollowCatcher/.agent/skills/clean-architecture/SKILL.md)**.
> For shared rules (Constructors, .NET Version), refer to **[.agent/skills/project-standards.md](file:///c:/Users/Burak/Documents/MY/GITHUB/BURAKERGUN394/projects/FollowCatcher/.agent/skills/project-standards.md)**.

## Placeholder Legend

- `{ProjectName}`: Your project name (e.g., `FollowCatcher`)
- `{Feature}`: Singular entity/feature name (e.g., `Employee`, `Vehicle`)
- `{Features}`: Plural entity/feature name (e.g., `Employees`, `Vehicles`)
- `{ValueObject}`: Value object name (e.g., `Email`, `Location`)

## Entity Creation Recipes

### 1. Basic Entity

```csharp
// Location: Domain/{Feature}/{Feature}.cs
namespace {ProjectName}.Domain.{Features};

public class {Feature} : Entity
{
    public string Property1 { get; private set; }
    public string Property2 { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    // Private constructor for EF Core
    private {Feature}() { }

    // Public constructor with validation
    public {Feature}(string property1, string property2)
    {
        Id = Guid.CreateVersion7(); // .NET 9+ timestamp-based GUID
        SetProperty1(property1);
        Property2 = property2;
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
}
```

### 2. Entity with Value Objects

```csharp
public class Employee : Entity
{
    public string Name { get; private set; }
    public Email Email { get; private set; }  // Value Object
    public Address Address { get; private set; }  // Value Object

    private Employee() { }

    public Employee(string name, Email email, Address address)
    {
        Id = Guid.CreateVersion7();
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        Address = address ?? throw new ArgumentNullException(nameof(address));
        
        // Raising Domain Event
        RaiseDomainEvent(new EmployeeCreatedEvent(Id, Name, email.Value));
    }
}
```

### 3. Value Object

```csharp
// Location: Domain/{Feature}/{ValueObject}.cs
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
    
    // Explicit conversion operator for easier usage if needed
    public static implicit operator string(Email email) => email.Value;
}
```

### 4. Aggregate Root with Collection

```csharp
public class Route : Entity // Aggregate Root
{
    public string Name { get; private set; }
    
    // Collection of member entities - only accessible through the root
    private readonly List<RouteStop> stops = [];
    public IReadOnlyCollection<RouteStop> Stops => stops.AsReadOnly();

    private Route() { }

    public Route(string name)
    {
        Id = Guid.CreateVersion7();
        Name = name;
    }

    // Encapsulated collection modification
    public void AddStop(Guid employeeId, int order)
    {
        if (stops.Any(s => s.EmployeeId == employeeId))
            throw new DomainException("Employee already added to route");

        var stop = new RouteStop(Id, employeeId, order);
        stops.Add(stop);
        
        RaiseDomainEvent(new StopAddedEvent(Id, employeeId, order));
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

        // Clustered index on Id for performance (Guid.CreateVersion7)
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id).IsClustered();

        builder.Property(x => x.Property1)
            .IsRequired()
            .HasMaxLength(100);

        // Value Object Configuration
        builder.OwnsOne(x => x.Email, email =>
        {
            email.Property(e => e.Value)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(255);
            email.HasIndex(e => e.Value).IsUnique();
        });
        
        // Collection Navigation
        builder.HasMany(x => x.Stops)
            .WithOne()
            .HasForeignKey("RouteId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
```

## Checklist

- [ ] Create generic `DomainEvent` base class (if not exists)
- [ ] Create `Entity` base class (if not exists)
- [ ] Create Feature folder in Domain Project
- [ ] Create Entity class (inherit from `Entity`)
- [ ] Define private setters and public behavior methods
- [ ] Create Value Objects (records)
- [ ] Create Domain Events (records)
- [ ] Create EF Core Configuration
