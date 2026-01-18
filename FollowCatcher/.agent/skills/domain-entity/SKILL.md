---
name: domain-entity
description: Create domain entities following DDD principles. Use when the user wants to create a new entity, add a domain model, implement domain logic, or mentions "entity", "domain", "model", "aggregate".
allowed-tools: Read, Grep, Glob, Write, Edit
---

# Domain Entity Creation Guide

**Project Requirements**: .NET 10, C# 14

## Placeholder Legend

This guide uses the following placeholders that should be replaced with your specific values:
- `{ProjectName}`: Your project name (e.g., `FollowCatcher`)
- `{Feature}`: Singular entity/feature name (e.g., `Employee`, `Vehicle`, `Route`)
- `{Features}`: Plural entity/feature name (e.g., `Employees`, `Vehicles`, `Routes`)
- `{ValueObject}`: Value object name (e.g., `Email`, `Location`, `Address`)

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

## Domain Events Pattern

Domain Events represent something that happened in the domain that domain experts care about. They enable loose coupling between aggregates and provide a way to react to state changes.

### Domain Event Base Class

```csharp
// Location: Domain/Common/DomainEvent.cs
public abstract class DomainEvent
{
    public Guid Id { get; } = Guid.CreateVersion7();
    public DateTime OccurredAt { get; } = DateTime.UtcNow;
}
```

### Concrete Domain Events

```csharp
// Location: Domain/Employees/EmployeeCreatedEvent.cs
public class EmployeeCreatedEvent : DomainEvent
{
    public Guid EmployeeId { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }

    public EmployeeCreatedEvent(Guid employeeId, string name, string email)
    {
        EmployeeId = employeeId;
        Name = name;
        Email = email;
    }
}

// Location: Domain/Employees/EmployeeDeletedEvent.cs
public class EmployeeDeletedEvent : DomainEvent
{
    public Guid EmployeeId { get; init; }

    public EmployeeDeletedEvent(Guid employeeId)
    {
        EmployeeId = employeeId;
    }
}
```

### Entity Base Class with Domain Events

```csharp
// Location: Domain/Common/Entity.cs
public abstract class Entity
{
    // C# 14 collection expression
    private readonly List<DomainEvent> domainEvents = [];
    public IReadOnlyCollection<DomainEvent> DomainEvents => domainEvents.AsReadOnly();

    protected void RaiseDomainEvent(DomainEvent domainEvent)
    {
        domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        domainEvents.Clear();
    }
}
```

### Using Domain Events in Entities

```csharp
public class Employee : Entity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Employee() { }

    public Employee(string name, Email email)
    {
        Id = Guid.CreateVersion7();
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        CreatedAt = DateTime.UtcNow;

        // Raise domain event
        RaiseDomainEvent(new EmployeeCreatedEvent(Id, Name, email.Value));
    }

    public void Delete()
    {
        RaiseDomainEvent(new EmployeeDeletedEvent(Id));
    }
}
```

### Publishing Domain Events

Domain events should be published after successful transaction commit. Use a domain event dispatcher in your DbContext:

```csharp
// Location: Persistence/ApplicationDbContext.cs
public class ApplicationDbContext : DbContext
{
    private readonly IMediator mediator;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator)
        : base(options)
    {
        this.mediator = mediator;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Get all entities with domain events
        var entitiesWithEvents = ChangeTracker.Entries<Entity>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity)
            .ToList();

        // Get all domain events
        var domainEvents = entitiesWithEvents
            .SelectMany(e => e.DomainEvents)
            .ToList();

        // Clear events before saving
        foreach (var entity in entitiesWithEvents)
        {
            entity.ClearDomainEvents();
        }

        // Save changes
        var result = await base.SaveChangesAsync(cancellationToken);

        // Publish events after successful save
        foreach (var domainEvent in domainEvents)
        {
            await this.mediator.Publish(domainEvent, cancellationToken);
        }

        return result;
    }
}
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
        CreatedAt = DateTime.UtcNow;
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

## Aggregate Root Pattern

An **Aggregate Root** is the main entity in an aggregate cluster that controls access to all other entities within the aggregate. It enforces business rules and maintains consistency across the aggregate boundary.

### Key Principles

1. **External Access**: External objects can only hold references to the aggregate root
2. **Consistency Boundary**: The aggregate root enforces invariants across all entities in the aggregate
3. **Transaction Boundary**: Changes to the aggregate are committed as a single transaction
4. **Cascade Operations**: Deleting the aggregate root deletes all member entities

### Aggregate Root Example

```csharp
// Route is the Aggregate Root
// RouteStop is a member entity (only accessible through Route)
public class Route : Entity  // Aggregate Root
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Guid VehicleId { get; private set; }

    // Collection of member entities - only accessible through the root
    private readonly List<RouteStop> stops = [];
    public IReadOnlyCollection<RouteStop> Stops => stops.AsReadOnly();

    private Route() { }

    public Route(string name, Guid vehicleId)
    {
        Id = Guid.CreateVersion7();
        SetName(name);
        VehicleId = vehicleId;
    }

    // Aggregate root enforces business rules
    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Route name cannot be empty");

        Name = name;
        RaiseDomainEvent(new RouteNameChangedEvent(Id, name));
    }

    // All access to member entities goes through the root
    public void AddStop(Employee employee, int order)
    {
        // Validate business rules
        if (stops.Any(s => s.EmployeeId == employee.Id))
            throw new DomainException("Employee already added to route");

        if (order < 1)
            throw new DomainException("Stop order must be positive");

        var stop = new RouteStop(Id, employee.Id, order);
        stops.Add(stop);

        RaiseDomainEvent(new StopAddedEvent(Id, employee.Id, order));
    }

    public void RemoveStop(Guid employeeId)
    {
        var stop = stops.FirstOrDefault(s => s.EmployeeId == employeeId);
        if (stop == null)
            throw new DomainException("Stop not found");

        stops.Remove(stop);
        RaiseDomainEvent(new StopRemovedEvent(Id, employeeId));
    }

    // Reorder stops while maintaining consistency
    public void ReorderStops(Dictionary<Guid, int> newOrders)
    {
        foreach (var stop in stops)
        {
            if (!newOrders.ContainsKey(stop.EmployeeId))
                throw new DomainException($"Missing order for employee {stop.EmployeeId}");

            stop.UpdateOrder(newOrders[stop.EmployeeId]);
        }
    }
}

// Member entity - internal to the aggregate
public class RouteStop
{
    public Guid RouteId { get; private set; }
    public Guid EmployeeId { get; private set; }
    public int Order { get; private set; }

    // Internal constructor - only the aggregate root can create instances
    internal RouteStop(Guid routeId, Guid employeeId, int order)
    {
        RouteId = routeId;
        EmployeeId = employeeId;
        Order = order;
    }

    // Internal method - only called by aggregate root
    internal void UpdateOrder(int newOrder)
    {
        if (newOrder < 1)
            throw new DomainException("Order must be positive");

        Order = newOrder;
    }
}
```

### Aggregate Design Guidelines

**DO:**
- Keep aggregates small (fewer entities = better performance)
- Use IDs to reference other aggregates (not object references)
- Enforce all business rules in the aggregate root
- Raise domain events for significant state changes

**DON'T:**
- Don't create large aggregates with many entities
- Don't allow direct access to member entities from outside
- Don't modify multiple aggregates in a single transaction (use eventual consistency)
- Don't hold references to other aggregate roots (use IDs instead)

### Entity with Navigation Properties

```csharp
public class Route
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Guid VehicleId { get; private set; }
    public Vehicle Vehicle { get; private set; }  // Navigation

    // C# 14 collection expression syntax
    private readonly List<RouteStop> stops = [];
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

        // Primary Key - Guid.CreateVersion7() provides natural clustering
        builder.HasKey(x => x.Id);

        // Clustered index on Id (default for primary key)
        // Guid.CreateVersion7() naturally orders by creation time
        // This improves insert performance and reduces page splits
        builder.HasIndex(x => x.Id).IsClustered();

        builder.Property(x => x.Property1)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Property2)
            .HasMaxLength(500);

        // Concurrency token - prevents concurrent update conflicts
        builder.Property(x => x.UpdatedAt)
            .IsConcurrencyToken();

        // Soft delete query filter
        builder.HasQueryFilter(x => x.DeletedAt == null);

        // Index on DeletedAt for soft delete queries
        builder.HasIndex(x => x.DeletedAt);

        // Value Object configuration - Location
        builder.OwnsOne(x => x.Location, location =>
        {
            location.Property(l => l.Latitude)
                .HasColumnName("Latitude")
                .IsRequired();

            location.Property(l => l.Longitude)
                .HasColumnName("Longitude")
                .IsRequired();
        });

        // Value Object configuration - Email
        builder.OwnsOne(x => x.Email, email =>
        {
            email.Property(e => e.Value)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(255);

            // Unique index on email
            email.HasIndex(e => e.Value).IsUnique();
        });

        // Relationships
        builder.HasMany(x => x.Stops)
            .WithOne()
            .HasForeignKey(x => x.RouteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
```

### Advanced EF Core Configuration Examples

#### Guid.CreateVersion7() Clustered Index Strategy

```csharp
public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        // Guid.CreateVersion7() benefits from clustered index
        // Time-ordered GUIDs reduce page splits and improve insert performance
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id).IsClustered();

        // For queries by other columns, add non-clustered indexes
        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.CreatedAt);
    }
}
```

#### Optimistic Concurrency Control

```csharp
public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        // Option 1: Use UpdatedAt as concurrency token
        builder.Property(x => x.UpdatedAt)
            .IsConcurrencyToken();

        // Option 2: Use RowVersion (SQL Server)
        builder.Property<byte[]>("RowVersion")
            .IsRowVersion()
            .IsConcurrencyToken();
    }
}
```

#### Value Object Conversions

```csharp
public class RouteConfiguration : IEntityTypeConfiguration<Route>
{
    public void Configure(EntityTypeBuilder<Route> builder)
    {
        // Complex value object with validation
        builder.OwnsOne(x => x.StartLocation, location =>
        {
            location.Property(l => l.Latitude)
                .HasColumnName("StartLatitude")
                .HasPrecision(9, 6);  // 6 decimal places for GPS coordinates

            location.Property(l => l.Longitude)
                .HasColumnName("StartLongitude")
                .HasPrecision(9, 6);
        });

        // Multiple value objects of same type
        builder.OwnsOne(x => x.EndLocation, location =>
        {
            location.Property(l => l.Latitude)
                .HasColumnName("EndLatitude")
                .HasPrecision(9, 6);

            location.Property(l => l.Longitude)
                .HasColumnName("EndLongitude")
                .HasPrecision(9, 6);
        });
    }
}
```

## Repository Interface Pattern

Repositories provide an abstraction over data access, allowing the domain layer to remain ignorant of persistence concerns. Repository interfaces are defined in the Domain layer and implemented in the Persistence layer.

### Generic Repository Interface

```csharp
// Location: Domain/Common/IRepository.cs
public interface IRepository<T> where T : Entity
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    void Update(T entity);
    void Remove(T entity);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}
```

### Entity-Specific Repository Interface

```csharp
// Location: Domain/Employees/IEmployeeRepository.cs
public interface IEmployeeRepository : IRepository<Employee>
{
    Task<IEnumerable<Employee>> GetByLocationAsync(Location location, CancellationToken cancellationToken = default);
    Task<Employee?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<IEnumerable<Employee>> SearchByNameAsync(string searchTerm, CancellationToken cancellationToken = default);
}
```

### Repository Implementation

```csharp
// Location: Persistence/Repositories/EmployeeRepository.cs
public class EmployeeRepository(ApplicationDbContext context) : IEmployeeRepository
{
    public async Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Employees
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Employees
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Employee entity, CancellationToken cancellationToken = default)
    {
        await context.Employees.AddAsync(entity, cancellationToken);
    }

    public void Update(Employee entity)
    {
        context.Employees.Update(entity);
    }

    public void Remove(Employee entity)
    {
        context.Employees.Remove(entity);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Employees
            .AnyAsync(e => e.Id == id, cancellationToken);
    }

    // Entity-specific methods
    public async Task<IEnumerable<Employee>> GetByLocationAsync(Location location, CancellationToken cancellationToken = default)
    {
        return await context.Employees
            .Where(e => e.Location == location)
            .ToListAsync(cancellationToken);
    }

    public async Task<Employee?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await context.Employees
            .FirstOrDefaultAsync(e => e.Email.Value == email, cancellationToken);
    }

    public async Task<IEnumerable<Employee>> SearchByNameAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        return await context.Employees
            .Where(e => e.Name.Contains(searchTerm))
            .ToListAsync(cancellationToken);
    }
}
```

### Repository Registration

```csharp
// Location: Api/Program.cs or Persistence/DependencyInjection.cs
services.AddScoped<IEmployeeRepository, EmployeeRepository>();
services.AddScoped<IRouteRepository, RouteRepository>();
services.AddScoped<IVehicleRepository, VehicleRepository>();
```

### Best Practices

**DO:**
- Define repository interfaces in the Domain layer
- Implement repositories in the Persistence layer
- Keep repository methods focused on data access
- Use specific repository interfaces for complex queries
- Include CancellationToken in all async methods

**DON'T:**
- Don't put business logic in repositories
- Don't return IQueryable from repositories (return materialized results)
- Don't create a repository for every entity (only aggregates need repositories)
- Don't expose EF Core-specific types in repository interfaces

## Soft-Delete Pattern

Soft delete marks records as deleted without physically removing them from the database. This preserves data for auditing and allows for potential recovery.

### Entity with Soft Delete

```csharp
public class Employee : Entity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public bool IsDeleted => DeletedAt.HasValue;

    private Employee() { }

    public Employee(string name, Email email)
    {
        Id = Guid.CreateVersion7();
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        CreatedAt = DateTime.UtcNow;

        RaiseDomainEvent(new EmployeeCreatedEvent(Id, Name, email.Value));
    }

    public void Delete()
    {
        if (DeletedAt.HasValue)
            throw new DomainException("Employee is already deleted");

        DeletedAt = DateTime.UtcNow;
        RaiseDomainEvent(new EmployeeDeletedEvent(Id));
    }

    public void Restore()
    {
        if (!DeletedAt.HasValue)
            throw new DomainException("Employee is not deleted");

        DeletedAt = null;
        UpdatedAt = DateTime.UtcNow;
        RaiseDomainEvent(new EmployeeRestoredEvent(Id));
    }
}
```

### EF Core Configuration for Soft Delete

```csharp
public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        // Soft delete query filter - excludes deleted records by default
        builder.HasQueryFilter(x => x.DeletedAt == null);

        // Index on DeletedAt for better performance
        builder.HasIndex(x => x.DeletedAt);
    }
}
```

### Repository with Soft Delete

```csharp
public class EmployeeRepository(ApplicationDbContext context) : IEmployeeRepository
{
    // Returns only non-deleted employees (query filter applied automatically)
    public async Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Employees
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    // Get including deleted employees (ignores query filter)
    public async Task<Employee?> GetByIdIncludingDeletedAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Employees
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    // Soft delete
    public void Remove(Employee entity)
    {
        entity.Delete();  // Calls Delete() method, not context.Remove()
        context.Employees.Update(entity);
    }

    // Hard delete (permanent removal)
    public void HardDelete(Employee entity)
    {
        context.Employees.Remove(entity);
    }

    // Get deleted employees
    public async Task<IEnumerable<Employee>> GetDeletedAsync(CancellationToken cancellationToken = default)
    {
        return await context.Employees
            .IgnoreQueryFilters()
            .Where(e => e.DeletedAt != null)
            .ToListAsync(cancellationToken);
    }
}
```

### Soft Delete Best Practices

**DO:**
- Use `HasQueryFilter` to automatically exclude deleted records
- Add index on `DeletedAt` for performance
- Provide methods to query deleted records when needed (with `IgnoreQueryFilters`)
- Raise domain events for delete and restore operations
- Consider cascading soft deletes for related entities

**DON'T:**
- Don't use soft delete for all entities (only where audit trail is important)
- Don't forget to handle foreign key relationships with soft-deleted records
- Don't allow deletion of already-deleted records
- Don't expose hard delete to application layer unless absolutely necessary

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
