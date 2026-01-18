---
name: clean-architecture
description: Guide for implementing Clean Architecture patterns in .NET projects. Use when creating new features, refactoring code structure, or when the user mentions "clean architecture", "layers", "domain-driven", "CQRS", or asks about project structure.
allowed-tools: Read, Grep, Glob, Write, Edit
---

# Clean Architecture Guide for .NET Projects

**Project Requirements**: .NET 10, C# 14

## Placeholder Legend

This guide uses the following placeholders that should be replaced with your specific values:
- `{ProjectName}`: Your project name (e.g., `FollowCatcher`)
- `{Feature}`: Singular entity/feature name (e.g., `Employee`, `Vehicle`, `Route`)

## Architecture Layers

This project follows Clean Architecture with the following layers:

### 1. Domain Layer (`{ProjectName}.Domain`)
- **Purpose**: Core business logic, entities, and domain rules
- **Dependencies**: None (innermost layer)
- **Contains**:
  - Entities (e.g., `Employee`, `Vehicle`, `Route`)
  - Value Objects
  - Domain Events
  - Domain Exceptions
  - Repository Interfaces

### 2. Application Layer (`{ProjectName}.Application`)
- **Purpose**: Use cases, business workflows, CQRS handlers
- **Dependencies**: Domain layer only
- **Contains**:
  - Commands and Handlers (write operations)
  - Queries and Handlers (read operations)
  - DTOs (Data Transfer Objects)
  - Validators (FluentValidation)
  - Application Interfaces

### 3. Persistence Layer (`{ProjectName}.Persistence`)
- **Purpose**: Database access, repositories, and data persistence
- **Dependencies**: Domain and Application layers
- **Contains**:
  - DbContext
  - Entity Configurations (EF Core)
  - Repository Implementations
  - Migrations

### 4. Infrastructure Layer (`{ProjectName}.Infrastructure`)
- **Purpose**: External services, file system, non-database infrastructure
- **Dependencies**: Application layer
- **Contains**:
  - External Service Implementations (e.g., Email, Instagram)
  - File System Adapters
  - System Clock

### 5. API Layer (`{ProjectName}.Api`)
- **Purpose**: HTTP endpoints, request/response handling
- **Dependencies**: All layers (composition root)
- **Contains**:
  - Controllers
  - Middleware
  - Filters
  - DI Configuration

## CQRS Pattern

CQRS (Command Query Responsibility Segregation) separates read and write operations for better scalability and maintainability.

### Commands (Write Operations)

Commands modify state and typically return identifiers or success indicators.

```csharp
// Command definition
public record CreateEmployeeCommand(string Name, string Email) : IRequest<Guid>;

// Simple handler - basic create operation
public class CreateEmployeeHandler(IApplicationDbContext context)
    : IRequestHandler<CreateEmployeeCommand, Guid>
{
    public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken ct)
    {
        var employee = new Employee(request.Name, new Email(request.Email));
        context.Employees.Add(employee);
        await context.SaveChangesAsync(ct);
        return employee.Id;
    }
}

// Handler with logging
public class CreateEmployeeWithLoggingHandler(
    IApplicationDbContext context,
    ILogger<CreateEmployeeWithLoggingHandler> logger)
    : IRequestHandler<CreateEmployeeCommand, Guid>
{
    public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken ct)
    {
        logger.LogInformation("Creating employee with name {Name}", request.Name);

        var employee = new Employee(request.Name, new Email(request.Email));
        context.Employees.Add(employee);
        await context.SaveChangesAsync(ct);

        logger.LogInformation("Employee created with ID {EmployeeId}", employee.Id);
        return employee.Id;
    }
}

// Handler with repository and error handling
public class UpdateEmployeeHandler(
    IEmployeeRepository repository,
    IUnitOfWork unitOfWork,
    ILogger<UpdateEmployeeHandler> logger)
    : IRequestHandler<UpdateEmployeeCommand>
{
    public async Task Handle(UpdateEmployeeCommand request, CancellationToken ct)
    {
        logger.LogInformation("Updating employee {EmployeeId}", request.Id);

        var employee = await repository.GetByIdAsync(request.Id, ct);
        if (employee == null)
        {
            logger.LogWarning("Employee {EmployeeId} not found", request.Id);
            throw new NotFoundException($"Employee with ID {request.Id} not found");
        }

        employee.UpdateName(request.Name);
        employee.UpdateEmail(new Email(request.Email));

        repository.Update(employee);
        await unitOfWork.SaveChangesAsync(ct);

        logger.LogInformation("Employee {EmployeeId} updated successfully", request.Id);
    }
}
```

### Queries (Read Operations)

Queries retrieve data without modifying state and should use `AsNoTracking()` for better performance.

```csharp
// Query definition
public record GetEmployeeByIdQuery(Guid Id) : IRequest<EmployeeDto>;

// Simple query handler
public class GetEmployeeByIdHandler(IApplicationDbContext context)
    : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>
{
    public async Task<EmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken ct)
    {
        var employee = await context.Employees
            .AsNoTracking()  // Performance optimization for read-only queries
            .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

        return employee is null
            ? null
            : new EmployeeDto(employee.Id, employee.Name, employee.Email.Value);
    }
}

// Query with projection and logging
public class GetEmployeeDetailsHandler(
    IApplicationDbContext context,
    ILogger<GetEmployeeDetailsHandler> logger)
    : IRequestHandler<GetEmployeeDetailsQuery, EmployeeDetailsDto>
{
    public async Task<EmployeeDetailsDto> Handle(GetEmployeeDetailsQuery request, CancellationToken ct)
    {
        logger.LogDebug("Fetching employee details for {EmployeeId}", request.Id);

        var employeeDto = await context.Employees
            .AsNoTracking()
            .Where(e => e.Id == request.Id)
            .Select(e => new EmployeeDetailsDto(
                e.Id,
                e.Name,
                e.Email.Value,
                e.Location,
                e.CreatedAt,
                e.UpdatedAt))
            .FirstOrDefaultAsync(ct);

        if (employeeDto == null)
        {
            logger.LogWarning("Employee {EmployeeId} not found", request.Id);
            throw new NotFoundException($"Employee with ID {request.Id} not found");
        }

        logger.LogDebug("Employee details fetched successfully");
        return employeeDto;
    }
}

// Query with complex filtering
public class GetEmployeesByLocationHandler(
    IApplicationDbContext context,
    ILogger<GetEmployeesByLocationHandler> logger)
    : IRequestHandler<GetEmployeesByLocationQuery, IEnumerable<EmployeeDto>>
{
    public async Task<IEnumerable<EmployeeDto>> Handle(
        GetEmployeesByLocationQuery request,
        CancellationToken ct)
    {
        logger.LogInformation("Fetching employees for location {Location}", request.Location);

        var employees = await context.Employees
            .AsNoTracking()
            .Where(e => e.Location == request.Location)
            .OrderBy(e => e.Name)
            .Select(e => new EmployeeDto(e.Id, e.Name, e.Email.Value))
            .ToListAsync(ct);

        logger.LogInformation("Found {Count} employees", employees.Count);
        return employees;
    }
}
```

### CQRS Best Practices

**Commands:**
- Return only identifiers or success indicators, not full entities
- Use domain entities to enforce business rules
- Validate input using FluentValidation
- Log important state changes
- Raise domain events for significant operations

**Queries:**
- Always use `AsNoTracking()` for read-only operations
- Project directly to DTOs using `Select()`
- Never return domain entities from queries
- Use pagination for large result sets
- Cache frequently accessed data when appropriate

**General:**
- Keep handlers focused on single responsibility
- Use primary constructors (C# 14) for cleaner code
- Include `CancellationToken` in all async operations
- Log at appropriate levels (Debug for details, Information for important events, Warning for issues)
- Handle exceptions appropriately (not found, validation errors, etc.)

## Unit of Work Pattern

The Unit of Work pattern maintains a list of objects affected by a business transaction and coordinates writing changes to the database. In Clean Architecture with EF Core, the DbContext implements the Unit of Work pattern.

### IUnitOfWork Interface

```csharp
// Location: Application/Common/Interfaces/IUnitOfWork.cs
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync();
}
```

### DbContext as Unit of Work

```csharp
// Location: Persistence/ApplicationDbContext.cs
public class ApplicationDbContext : DbContext, IUnitOfWork
{
    private IDbContextTransaction? currentTransaction;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Route> Routes => Set<Route>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Publish domain events before saving
        await PublishDomainEventsAsync(cancellationToken);

        return await base.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (currentTransaction != null)
            throw new InvalidOperationException("Transaction already started");

        currentTransaction = await Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (currentTransaction == null)
            throw new InvalidOperationException("No transaction started");

        try
        {
            await SaveChangesAsync(cancellationToken);
            await currentTransaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            currentTransaction?.Dispose();
            currentTransaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (currentTransaction == null)
            return;

        await currentTransaction.RollbackAsync();
        currentTransaction?.Dispose();
        currentTransaction = null;
    }

    private async Task PublishDomainEventsAsync(CancellationToken cancellationToken)
    {
        var domainEvents = ChangeTracker.Entries<Entity>()
            .Select(x => x.Entity)
            .Where(x => x.DomainEvents.Any())
            .SelectMany(x => x.DomainEvents)
            .ToList();

        foreach (var domainEvent in domainEvents)
        {
            // Publish using MediatR or your event dispatcher
            // await mediator.Publish(domainEvent, cancellationToken);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
```

### Using Unit of Work in Handlers

```csharp
// Simple handler - automatic transaction
public class CreateEmployeeHandler(IApplicationDbContext context)
    : IRequestHandler<CreateEmployeeCommand, Guid>
{
    public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken ct)
    {
        var employee = new Employee(request.Name, new Email(request.Email));
        context.Employees.Add(employee);

        // SaveChangesAsync creates implicit transaction
        await context.SaveChangesAsync(ct);

        return employee.Id;
    }
}

// Complex handler - explicit transaction for multiple operations
public class TransferEmployeeHandler(IUnitOfWork unitOfWork, IEmployeeRepository employeeRepo)
    : IRequestHandler<TransferEmployeeCommand>
{
    public async Task Handle(TransferEmployeeCommand request, CancellationToken ct)
    {
        await unitOfWork.BeginTransactionAsync(ct);

        try
        {
            var employee = await employeeRepo.GetByIdAsync(request.EmployeeId, ct);
            if (employee == null)
                throw new NotFoundException("Employee not found");

            // Multiple operations in one transaction
            employee.UpdateLocation(request.NewLocation);
            await unitOfWork.SaveChangesAsync(ct);

            // Create audit log
            var auditLog = new AuditLog(employee.Id, "Transfer", DateTime.UtcNow);
            // await auditRepo.AddAsync(auditLog, ct);

            await unitOfWork.CommitTransactionAsync(ct);
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
```

### Unit of Work Best Practices

**DO:**
- Use SaveChangesAsync for single-aggregate operations (implicit transaction)
- Use explicit transactions for multi-aggregate operations
- Publish domain events after SaveChangesAsync but before transaction commit
- Rollback transaction on any failure in multi-step operations

**DON'T:**
- Don't nest transactions (check if transaction already exists)
- Don't forget to dispose transactions in finally blocks
- Don't mix multiple DbContext instances in one Unit of Work
- Don't use Unit of Work for read-only queries

## Specification Pattern

The Specification pattern encapsulates query logic into reusable, composable, and testable objects. It's particularly useful for complex filtering and business rule evaluation.

### Base Specification

```csharp
// Location: Domain/Common/Specification.cs
public abstract class Specification<T>
{
    public abstract Expression<Func<T, bool>> ToExpression();

    public bool IsSatisfiedBy(T entity)
    {
        Func<T, bool> predicate = ToExpression().Compile();
        return predicate(entity);
    }

    public Specification<T> And(Specification<T> specification)
    {
        return new AndSpecification<T>(this, specification);
    }

    public Specification<T> Or(Specification<T> specification)
    {
        return new OrSpecification<T>(this, specification);
    }

    public Specification<T> Not()
    {
        return new NotSpecification<T>(this);
    }
}

// Composite specifications
internal class AndSpecification<T>(Specification<T> left, Specification<T> right) : Specification<T>
{
    public override Expression<Func<T, bool>> ToExpression()
    {
        Expression<Func<T, bool>> leftExpression = left.ToExpression();
        Expression<Func<T, bool>> rightExpression = right.ToExpression();

        var parameter = Expression.Parameter(typeof(T));
        var body = Expression.AndAlso(
            Expression.Invoke(leftExpression, parameter),
            Expression.Invoke(rightExpression, parameter));

        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }
}

internal class OrSpecification<T>(Specification<T> left, Specification<T> right) : Specification<T>
{
    public override Expression<Func<T, bool>> ToExpression()
    {
        Expression<Func<T, bool>> leftExpression = left.ToExpression();
        Expression<Func<T, bool>> rightExpression = right.ToExpression();

        var parameter = Expression.Parameter(typeof(T));
        var body = Expression.OrElse(
            Expression.Invoke(leftExpression, parameter),
            Expression.Invoke(rightExpression, parameter));

        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }
}

internal class NotSpecification<T>(Specification<T> specification) : Specification<T>
{
    public override Expression<Func<T, bool>> ToExpression()
    {
        Expression<Func<T, bool>> expression = specification.ToExpression();
        var parameter = Expression.Parameter(typeof(T));
        var body = Expression.Not(Expression.Invoke(expression, parameter));

        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }
}
```

### Concrete Specifications

```csharp
// Location: Domain/Employees/Specifications/EmployeeByLocationSpec.cs
public class EmployeeByLocationSpec(Location location) : Specification<Employee>
{
    public override Expression<Func<Employee, bool>> ToExpression()
    {
        return e => e.Location == location;
    }
}

// Location: Domain/Employees/Specifications/ActiveEmployeeSpec.cs
public class ActiveEmployeeSpec : Specification<Employee>
{
    public override Expression<Func<Employee, bool>> ToExpression()
    {
        return e => e.DeletedAt == null;
    }
}

// Location: Domain/Employees/Specifications/EmployeeByNameSpec.cs
public class EmployeeByNameSpec(string searchTerm) : Specification<Employee>
{
    public override Expression<Func<Employee, bool>> ToExpression()
    {
        return e => e.Name.Contains(searchTerm);
    }
}
```

### Using Specifications in Repositories

```csharp
// Enhanced repository interface
public interface IEmployeeRepository : IRepository<Employee>
{
    Task<IEnumerable<Employee>> FindAsync(Specification<Employee> specification, CancellationToken ct = default);
    Task<Employee?> FindOneAsync(Specification<Employee> specification, CancellationToken ct = default);
    Task<int> CountAsync(Specification<Employee> specification, CancellationToken ct = default);
}

// Repository implementation
public class EmployeeRepository(ApplicationDbContext context) : IEmployeeRepository
{
    public async Task<IEnumerable<Employee>> FindAsync(
        Specification<Employee> specification,
        CancellationToken ct = default)
    {
        return await context.Employees
            .Where(specification.ToExpression())
            .ToListAsync(ct);
    }

    public async Task<Employee?> FindOneAsync(
        Specification<Employee> specification,
        CancellationToken ct = default)
    {
        return await context.Employees
            .Where(specification.ToExpression())
            .FirstOrDefaultAsync(ct);
    }

    public async Task<int> CountAsync(
        Specification<Employee> specification,
        CancellationToken ct = default)
    {
        return await context.Employees
            .Where(specification.ToExpression())
            .CountAsync(ct);
    }
}
```

### Using Specifications in Handlers

```csharp
// Query handler using specifications
public class GetEmployeesByLocationHandler(IEmployeeRepository repository)
    : IRequestHandler<GetEmployeesByLocationQuery, IEnumerable<EmployeeDto>>
{
    public async Task<IEnumerable<EmployeeDto>> Handle(
        GetEmployeesByLocationQuery request,
        CancellationToken ct)
    {
        // Combine specifications using fluent API
        var spec = new EmployeeByLocationSpec(request.Location)
            .And(new ActiveEmployeeSpec());

        var employees = await repository.FindAsync(spec, ct);

        return employees.Select(e => new EmployeeDto(e.Id, e.Name, e.Email.Value));
    }
}

// Complex specification composition
public class SearchEmployeesHandler(IEmployeeRepository repository)
    : IRequestHandler<SearchEmployeesQuery, IEnumerable<EmployeeDto>>
{
    public async Task<IEnumerable<EmployeeDto>> Handle(
        SearchEmployeesQuery request,
        CancellationToken ct)
    {
        var spec = new ActiveEmployeeSpec();

        // Add name filter if provided
        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            spec = spec.And(new EmployeeByNameSpec(request.SearchTerm));
        }

        // Add location filter if provided
        if (request.Location != null)
        {
            spec = spec.And(new EmployeeByLocationSpec(request.Location));
        }

        var employees = await repository.FindAsync(spec, ct);

        return employees.Select(e => new EmployeeDto(e.Id, e.Name, e.Email.Value));
    }
}
```

### Specification Best Practices

**DO:**
- Use specifications for complex business rules
- Combine specifications using And/Or/Not operators
- Keep specifications focused on single criteria
- Test specifications independently
- Use specifications in both queries and domain logic

**DON'T:**
- Don't put data access logic in specifications
- Don't create overly complex specification hierarchies
- Don't use specifications for simple single-property queries
- Don't duplicate specifications across different aggregates

## File Organization

```
src/
├── {ProjectName}.Domain/
│   ├── Employees/
│   │   └── Employee.cs
│   ├── Vehicles/
│   │   └── Vehicle.cs
│   └── Routes/
│       ├── Route.cs
│       └── RouteStop.cs
│
├── {ProjectName}.Application/
│   ├── Employees/
│   │   ├── Commands/
│   │   │   └── CreateEmployee/
│   │   │       ├── CreateEmployeeCommand.cs
│   │   │       └── CreateEmployeeHandler.cs
│   │   └── Queries/
│   │       └── GetEmployeeById/
│   │           ├── GetEmployeeByIdQuery.cs
│   │           └── GetEmployeeByIdHandler.cs
│   └── [Feature]/
│       ├── Commands/
│       ├── Queries/
│       └── [Feature]Dto.cs
│
├── {ProjectName}.Persistence/
│   ├── Configurations/
│   │   └── EmployeeConfiguration.cs
│   ├── Repositories/
│   │   └── EmployeeRepository.cs
│   └── ApplicationDbContext.cs
│
└── {ProjectName}.Api/
    └── Controllers/
        └── EmployeesController.cs
```

## Best Practices

1. **Dependency Rule**: Dependencies only point inward
2. **Domain Isolation**: Domain layer has no external dependencies
3. **Use Cases**: Each use case is a separate command/query
4. **Repository Pattern**: Abstract data access behind interfaces
5. **DTOs**: Never expose domain entities to API layer
6. **Validation**: Use FluentValidation for input validation
7. **Naming**: Use consistent naming (e.g., `Create[Entity]Command`)
8. **Constructors**: Always prefer Primary Constructors
