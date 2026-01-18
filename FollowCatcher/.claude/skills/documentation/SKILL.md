---
name: documentation
description: Generate code documentation, API docs, and technical guides. Use when documenting code, creating API documentation, writing technical specs, or when the user mentions "document", "docs", "README", "API docs", "swagger".
allowed-tools: Read, Grep, Glob, Write, Edit
---

# Documentation Guide

**Project Requirements**: .NET 10, C# 14

## Code Documentation

### Class Documentation

```csharp
public class Employee
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
}
```

### Method Documentation

```csharp
public async Task<Route> CreateRouteAsync(
    string name,
    Guid vehicleId,
    IEnumerable<Employee> stops)
{
    // Validate input parameters
    if (string.IsNullOrEmpty(name))
        throw new ArgumentNullException(nameof(name));

    // Create route with optimized stop order
    var route = new Route(name, vehicleId);

    // Add stops to route
    foreach (var employee in stops)
    {
        route.AddStop(employee);
    }

    return route;
}
```

## API Controller Documentation

```csharp
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class EmployeesController : ControllerBase
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmployeeDto>> GetById(Guid id)
    {
        // Implementation
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateEmployeeCommand command)
    {
        // Implementation
    }
}
```

## README Template

```markdown
# Project Name

Brief description of the project.

## Features

- Feature 1
- Feature 2
- Feature 3

## Prerequisites

- .NET 10 SDK
- SQL Server / PostgreSQL
- Docker (optional)

## Getting Started

### Installation

```bash
git clone https://github.com/username/project.git
cd project
dotnet restore
```

### Configuration

1. Copy `appsettings.example.json` to `appsettings.json`
2. Update the connection string
3. Set up required API keys

### Running the Application

```bash
cd src/ProjectName.Api
dotnet run
```

The API will be available at `https://localhost:5001`

## API Documentation

Swagger UI is available at `/swagger` when running in Development mode.

## Project Structure

```
src/
├── ProjectName.Api/          # API layer
├── ProjectName.Application/  # Business logic
├── ProjectName.Domain/       # Domain models
└── ProjectName.Persistence/  # Data access
```

## Contributing

1. Fork the repository
2. Create a feature branch
3. Submit a pull request

## License

MIT License
```

## API Endpoint Documentation

### Swagger/OpenAPI Configuration

```csharp
// In Program.cs
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "{Project Name} API",
        Version = "v1",
        Description = "API for managing {domain}",
        Contact = new OpenApiContact
        {
            Name = "Development Team",
            Email = "dev@example.com"
        }
    });
});
```

## Documentation Checklist

### Code Level
- [ ] Complex logic has inline comments explaining the "why"
- [ ] Non-obvious business rules are documented
- [ ] Edge cases and assumptions are noted

### API Level
- [ ] All endpoints have Swagger annotations
- [ ] Response codes are documented
- [ ] Request/response examples provided

### Project Level
- [ ] README with setup instructions
- [ ] Architecture documentation
- [ ] API reference documentation
- [ ] Deployment guide
