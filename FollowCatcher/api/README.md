# FollowCatcher API

A .NET 10 web API built with Clean Architecture principles, CQRS pattern, and Entity Framework Core.

## Architecture Overview

This project follows Clean Architecture with clear separation of concerns across four layers:

```
FollowCatcher.Api          ← Presentation Layer (Controllers, Middleware)
    ↓
FollowCatcher.Application  ← Application Layer (Use Cases, CQRS Handlers)
    ↓
FollowCatcher.Domain       ← Domain Layer (Entities, Business Rules)
    ↑
FollowCatcher.Persistence  ← Infrastructure Layer (Database, EF Core)
```

## Project Structure

### 1. Domain Layer (`FollowCatcher.Domain`)
**Purpose**: Core business logic and domain entities

**No external dependencies** - This is the innermost layer and should remain pure.

**Contains**:
- `Common/Entity.cs` - Base entity with domain events and soft delete support
- `Common/ValueObject.cs` - Base value object for DDD
- `Common/Specification.cs` - Specification pattern implementation
- `Common/DomainException.cs` - Domain-specific exceptions
- `Common/IRepository.cs` - Repository interfaces

### 2. Application Layer (`FollowCatcher.Application`)
**Purpose**: Use cases and business workflows using CQRS pattern

**Dependencies**: Domain layer only

**Contains**:
- `Common/Interfaces/IApplicationDbContext.cs` - Database context abstraction
- `Common/Interfaces/IUnitOfWork.cs` - Unit of Work pattern
- `DependencyInjection.cs` - Service registration

**Key Packages**:
- MediatR 14.0.0 - For CQRS implementation
- FluentValidation 12.1.1 - For command/query validation

### 3. Persistence Layer (`FollowCatcher.Persistence`)
**Purpose**: Database access and external service implementations

**Dependencies**: Domain and Application layers

**Contains**:
- `ApplicationDbContext.cs` - EF Core DbContext with Unit of Work
- `DependencyInjection.cs` - Database and repository registration

**Key Packages**:
- Microsoft.EntityFrameworkCore 10.0.2
- Microsoft.EntityFrameworkCore.SqlServer 10.0.2
- Microsoft.EntityFrameworkCore.Design 10.0.2

**Features**:
- Automatic timestamp management (CreatedAt, UpdatedAt)
- Domain event publishing
- Transaction support via Unit of Work
- Connection resilience with retry policy

### 4. API Layer (`FollowCatcher.Api`)
**Purpose**: HTTP endpoints and application composition

**Dependencies**: Application and Persistence layers

**Contains**:
- `Program.cs` - Application entry point and service configuration
- `appsettings.json` - Production configuration
- `appsettings.Development.json` - Development configuration

**Key Packages**:
- Swashbuckle.AspNetCore 10.1.0 - For Swagger/OpenAPI documentation

## Getting Started

### Prerequisites
- .NET 10 SDK
- SQL Server or LocalDB
- Visual Studio 2022 / VS Code / Rider

### Setup

1. **Update Connection String**

   Edit `appsettings.Development.json` or `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=FollowCatcherDb_Dev;Trusted_Connection=true"
     }
   }
   ```

2. **Build the Solution**
   ```bash
   cd api
   dotnet build
   ```

3. **Run Database Migrations** (when entities are added)
   ```bash
   cd src/FollowCatcher.Persistence
   dotnet ef migrations add InitialCreate --startup-project ../FollowCatcher.Api
   dotnet ef database update --startup-project ../FollowCatcher.Api
   ```

4. **Run the API**
   ```bash
   cd src/FollowCatcher.Api
   dotnet run
   ```

5. **Access Swagger UI**

   Navigate to: `https://localhost:<port>/swagger`

## Development Workflow

### Adding a New Feature

1. **Define Domain Entity** (using domain-entity skill)
   - Create entity in `Domain/{Feature}/` folder
   - Inherit from `Entity` base class
   - Define business rules and value objects

2. **Create Application Commands/Queries**
   - Commands in `Application/{Feature}/Commands/`
   - Queries in `Application/{Feature}/Queries/`
   - Use MediatR `IRequest<T>` and `IRequestHandler<T, R>`

3. **Create API Endpoint** (using api-endpoint skill)
   - Create controller in `Api/Controllers/`
   - Inject `IMediator` and send commands/queries

4. **Add Database Configuration** (if needed)
   - Create entity configuration in `Persistence/Configurations/`
   - Implement `IEntityTypeConfiguration<T>`

### Example: Creating a New Entity

```csharp
// 1. Domain Layer - Entity
public class Employee : Entity
{
    public Employee(string name, Email email)
    {
        Name = name;
        Email = email;
    }

    public string Name { get; private set; }
    public Email Email { get; private set; }

    public void UpdateName(string name)
    {
        Name = name;
        MarkAsUpdated();
    }
}

// 2. Application Layer - Command
public record CreateEmployeeCommand(string Name, string Email) : IRequest<Guid>;

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

// 3. API Layer - Controller
[ApiController]
[Route("api/[controller]")]
public class EmployeesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateEmployeeCommand command)
    {
        var id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }
}
```

## Design Patterns Used

- **Clean Architecture** - Dependency inversion and separation of concerns
- **CQRS** - Command Query Responsibility Segregation
- **Repository Pattern** - Data access abstraction
- **Unit of Work** - Transaction management
- **Specification Pattern** - Reusable query logic
- **Domain Events** - Decoupled domain logic
- **Dependency Injection** - Loose coupling and testability

## Best Practices

✅ **DO**:
- Keep Domain layer pure (no external dependencies)
- Use CQRS for clear separation of reads and writes
- Apply `AsNoTracking()` for read-only queries
- Use specifications for complex queries
- Validate commands with FluentValidation
- Return DTOs from queries, never domain entities
- Use primary constructors (C# 14)
- Include `CancellationToken` in async operations

❌ **DON'T**:
- Don't expose domain entities in API responses
- Don't skip validation on commands
- Don't use tracking for read operations
- Don't bypass the repository pattern
- Don't create god classes or services

## Tech Stack

- **.NET 10** - Latest .NET framework
- **C# 14** - Latest language features including primary constructors
- **Entity Framework Core 10** - ORM for database access
- **MediatR** - CQRS implementation
- **FluentValidation** - Input validation
- **Swashbuckle** - API documentation
- **SQL Server** - Database (configurable)

## Project Features

- ✅ Clean Architecture structure
- ✅ CQRS pattern with MediatR
- ✅ Entity Framework Core with SQL Server
- ✅ Domain Events support
- ✅ Specification Pattern
- ✅ Unit of Work pattern
- ✅ Soft Delete support
- ✅ Automatic timestamp management
- ✅ Connection resilience with retry policy
- ✅ Swagger/OpenAPI documentation
- ✅ Dependency Injection
- ✅ Nullable reference types enabled
- ✅ Primary constructors (C# 14)

## Next Steps

1. Add your first domain entity using the `domain-entity` skill
2. Create API endpoints using the `api-endpoint` skill
3. Add FluentValidation validators for commands
4. Implement repository pattern for your entities
5. Add integration tests
6. Configure CORS if needed for frontend
7. Add authentication and authorization
8. Implement logging and monitoring

## Resources

- [Clean Architecture by Robert C. Martin](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [CQRS Pattern](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs)
- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [MediatR Documentation](https://github.com/jbogard/MediatR)

## License

[Your License Here]
