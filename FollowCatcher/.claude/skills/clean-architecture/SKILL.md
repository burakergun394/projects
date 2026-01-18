---
name: clean-architecture
description: Guide for implementing Clean Architecture patterns in .NET projects. Use when creating new features, refactoring code structure, or when the user mentions "clean architecture", "layers", "domain-driven", "CQRS", or asks about project structure.
allowed-tools: Read, Grep, Glob, Write, Edit
---

# Clean Architecture Guide for .NET Projects

**Project Requirements**: .NET 10, C# 14

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

### 3. Infrastructure/Persistence Layer (`{ProjectName}.Persistence`)
- **Purpose**: External concerns, database access, external services
- **Dependencies**: Domain and Application layers
- **Contains**:
  - DbContext
  - Entity Configurations (EF Core)
  - Repository Implementations
  - External Service Implementations

### 4. API Layer (`{ProjectName}.Api`)
- **Purpose**: HTTP endpoints, request/response handling
- **Dependencies**: All layers (composition root)
- **Contains**:
  - Controllers
  - Middleware
  - Filters
  - DI Configuration

## CQRS Pattern

### Commands (Write Operations)
```csharp
// Command definition
public record CreateEmployeeCommand(string Name, string Email) : IRequest<Guid>;

// Handler
public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, Guid>
{
    public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        // Implementation
    }
}
```

### Queries (Read Operations)
```csharp
// Query definition
public record GetEmployeeByIdQuery(Guid Id) : IRequest<EmployeeDto>;

// Handler
public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>
{
    public async Task<EmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        // Implementation
    }
}
```

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
