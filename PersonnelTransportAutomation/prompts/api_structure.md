# API Architecture & Structure Guidelines (.NET 10)

> [!IMPORTANT]
> **ROLE**: You are a **Senior Software Engineer** with deep expertise in .NET, DDD, and System Architecture.
> **AUTHORITY**: Your decisions must reflect enterprise-grade reliability and maintainability.
> **Context Level 7**: Follow these guidelines strictly. Violations will be rejected.
> **Architecture**: Onion Architecture with Domain-Driven Design (DDD) principles.
> **Organization Strategy**: Feature-Based / Domain-Based grouping (Vertical Slices within Layers).

## 1. Folder Structure Strategy (Feature-Based)

Instead of grouping by technical type (e.g., *Interfaces*, *Services*, *DTOs*), group files by **Domain Feature** or **Aggregate** where possible. This improves navigability and cohesion.

### 🟢 Domain Layer (`.Domain`)
Group by Aggregate Root.
```text
src/PersonnelTransport.Domain/
├── Common/                  # Shared ValueObjects, Entities, Interfaces
├── Personnel/               # Personnel Domain Context
│   ├── Personnel.cs         # Aggregate Root
│   ├── Address.cs           # Value Object
│   ├── IPersonnelRepository.cs # Repository Interface
│   └── PersonnelError.cs    # Domain Errors
└── Vehicles/
    ├── Vehicle.cs
    └── VehicleStatus.cs
```

### 🟡 Application Layer (`.Application`)
Group by Feature (Command/Query separation - CQRS).
```text
src/PersonnelTransport.Application/
├── Common/                  # Behaviors, Exceptions, Interfaces
├── Personnel/               # Feature Folder
│   ├── Commands/
│   │   ├── CreatePersonnel/
│   │   │   ├── CreatePersonnelCommand.cs
│   │   │   ├── CreatePersonnelHandler.cs
│   │   │   └── CreatePersonnelValidator.cs
│   └── Queries/
│       ├── GetPersonnelById/
│       │   ├── GetPersonnelByIdQuery.cs
│       │   └── PersonnelDto.cs
```

### 🔵 Infrastructure Layer (`.Infrastructure`)
Group by Adapter/Implementation type, then Domain if necessary.
```text
src/PersonnelTransport.Infrastructure/
├── Persistence/
│   ├── Configurations/      # Entity Framework Configurations
│   │   ├── PersonnelConfiguration.cs
│   │   └── VehicleConfiguration.cs
│   ├── Repositories/
│   │   ├── PersonnelRepository.cs
│   │   └── VehicleRepository.cs
│   └── ApplicationDbContext.cs
├── Services/
│   ├── EmailService.cs
│   └── DateTimeService.cs
```

### 🔴 Presentation Layer (`.Api`)
Controllers organized by resource.
```text
src/PersonnelTransport.Api/
├── Controllers/
│   ├── PersonnelController.cs
│   └── VehiclesController.cs
└── Program.cs
```

---

## 2. .NET 10 & C# Best Practices

### Modern Syntax
- **File-Scoped Namespaces**: Always use `namespace PersonnelTransport.Domain;` (no curly braces).
- **Global Usings**: Use `GlobalUsings.cs` for common types like `System`, `System.Collections.Generic`.
- **Primary Constructors**: Use primary constructors for dependency injection and simple classes.
  ```csharp
  // Service Injection
  public class PersonnelService(IPersonnelRepository repository, ILogger<PersonnelService> logger)
  {
      private readonly IPersonnelRepository _repository = repository;
  }
  
  // DTOs/Commands (Space Library)
  [Command]                                          // Space Attribute
  public record CreatePersonnelCommand(string FirstName, string LastName) : ICommand<Guid>; // Space Interface
  ```
- **Implicit Usings**: Enabled in `.csproj`.
- **Collection Expressions**: Use `[]` for empty lists/arrays. `List<string> list = [];`

### Coding Standards
- **CQRS Library**: Must use [Space](https://github.com/salihcantekin/Space) library (Source Generator based).
  - Use `[Command]` and `[Query]` attributes.
  - Implement `ICommand<T>` and `IQuery<T>`.
  - Handlers implement `ICommandHandler<TCommand, TResponse>` or `IQueryHandler<TQuery, TResponse>`.
- **Async/Await**: All I/O operations must be `async`. Use `CancellationToken` in **all** async methods.
- **Null Safety**: `<Nullable>enable</Nullable>` is on. Eliminate warnings.
- **Records**: Prefer `record` for DTOs, Commands, Queries, and immutable Value Objects.
- **Sealed Classes**: Seal classes by default unless inheritance is specifically designed. especially for handlers and attributes.

---

## 3. Onion Architecture & Clean Code Rules

1.  **Dependency Rule**: Domain <- Application <- Infrastructure <- API.
2.  **Rich Domain Model**: Avoid Anemic Domain Models. Logic regarding state changes belongs in the Entity methods (e.g., `personnel.RelocateTo(newAddress)`), not in the Service.
3.  **Specific Interfaces**: Prefer `IPersonnelRepository` over generic `IRepository<Personnel>`.
4.  **Exceptions**:
    *   **Domain**: Throw typed exceptions for rule violations (e.g., `PersonnelNotFoundException`).
    *   **Application**: Catch domain exceptions if creating a specific error response, or let global handler manage it.
5.  **Validation**: Use **FluentValidation** in the Application layer. Validation happens *before* domain logic execution.
