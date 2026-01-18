---
name: documentation
description: Generate code documentation, API docs, and technical guides. Use when documenting code, creating API documentation, writing technical specs, or when the user mentions "document", "docs", "README", "API docs", "swagger".
allowed-tools: Read, Grep, Glob, Write, Edit
---

# Documentation Guide

**Project Requirements**: .NET 10, C# 14

## Code Documentation

## Inline Comment Philosophy

**Core Principle**: Comments should explain **WHY**, not **WHAT**. Code should be self-documenting through clear naming and structure.

### When to Comment

**✅ Good Reasons to Add Comments:**
- Complex business rules that aren't obvious from code alone
- Non-obvious performance optimizations
- Workarounds for framework limitations or bugs
- Security-critical operations and their rationale
- Intentional deviations from standards
- Algorithms with academic or external references
- Edge cases and their handling

**❌ Bad Reasons to Add Comments:**
- Repeating what the code obviously does
- Describing method or variable names
- Outdated information (delete instead)
- Apologizing for bad code (refactor instead)
- Commented-out code (use version control)

### Examples

**❌ Bad Comments (Explain the obvious):**
```csharp
// Set name to value
Name = value;

// Get employee by ID
var employee = await this.repository.GetByIdAsync(id);

// Loop through employees
foreach (var employee in employees)
{
    // Process employee
    ProcessEmployee(employee);
}

// Create new GUID
var id = Guid.CreateVersion7();
```

**✅ Good Comments (Explain the WHY):**
```csharp
// Normalize email to lowercase for case-insensitive comparison in database
// (database collation is case-sensitive for performance reasons)
Email = value.ToLowerInvariant();

// Use AsNoTracking to avoid change tracking overhead for read-only reporting query
// Performance improvement: ~40% faster for large result sets
var employees = await this.context.Employees
    .AsNoTracking()
    .ToListAsync(ct);

// Retry logic for transient failures (network timeouts, deadlocks)
// Max 3 attempts with exponential backoff: 100ms, 200ms, 400ms
await Policy
    .Handle<DbUpdateException>()
    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromMilliseconds(Math.Pow(2, retryAttempt) * 50))
    .ExecuteAsync(async () => await this.context.SaveChangesAsync(ct));

// SECURITY: Validate file extension against whitelist before processing upload
// Prevents malicious file execution attacks
var allowedExtensions = new[] { ".pdf", ".docx", ".xlsx" };
if (!allowedExtensions.Contains(Path.GetExtension(fileName).ToLower()))
    throw new ValidationException("File type not allowed");

// Workaround: EF Core 8 doesn't support filtered includes on collections
// TODO: Remove when upgrading to EF Core 9 (supports this natively)
var orders = await this.context.Orders
    .Include(o => o.Items)
    .ToListAsync(ct);
var filteredOrders = orders.Select(o => new
{
    Order = o,
    Items = o.Items.Where(i => i.IsActive).ToList()
});
```

**Complex Business Logic Example:**
```csharp
public decimal CalculateTax(decimal amount, string region)
{
    // Tax calculation follows IRS Publication 542 (2024)
    // Regional multipliers account for state and local taxes
    // Formula: base_rate * (1 + regional_modifier) * amount
    // Example: $100 in CA = 0.0825 * 1.0 * 100 = $8.25

    var baseRate = GetFederalTaxRate(amount);
    var regionalModifier = GetRegionalModifier(region);

    return baseRate * (1 + regionalModifier) * amount;
}
```

### Comment Maintenance

- **Update or Delete**: Comments must stay in sync with code. Outdated comments are worse than no comments.
- **Remove Commented Code**: Use version control (git) instead of commenting out code.
- **TODO Comments**: Include ticket number and date: `// TODO(TASK-123, 2024-01-15): Refactor to use new API`

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

## Database Setup

### Apply Migrations

```bash
cd src/ProjectName.Infrastructure
dotnet ef database update
```

### Create New Migration

```bash
dotnet ef migrations add MigrationName -s ../ProjectName.Api
```

### Seed Initial Data

```bash
dotnet run --project src/ProjectName.Api -- seed-data
```

## Environment Variables

Create a `.env` file in the API project root or configure in your hosting environment:

```env
# Database
ConnectionStrings__DefaultConnection=Server=localhost;Database=YourDb;User Id=sa;Password=YourPassword;TrustServerCertificate=True

# JWT Configuration
Jwt__Secret=your-super-secret-key-min-32-characters-long
Jwt__Issuer=your-app-name
Jwt__Audience=your-app-name
Jwt__ExpirationMinutes=60

# External APIs (if applicable)
GoogleMaps__ApiKey=your-api-key
SendGrid__ApiKey=your-sendgrid-key

# Application Settings
Environment=Development
AllowedHosts=*
```

**Security Notes:**
- Never commit `.env` or `appsettings.Production.json` to version control
- Use Azure Key Vault, AWS Secrets Manager, or similar for production secrets
- Use User Secrets for local development: `dotnet user-secrets set "Jwt:Secret" "your-secret"`

## Troubleshooting

### Common Issues

**Issue**: "Cannot connect to database"
**Solutions**:
- Check connection string in `appsettings.json` or environment variables
- Verify SQL Server / PostgreSQL is running: `docker ps` or check Windows Services
- Run migrations: `dotnet ef database update`
- Check firewall settings and database server accessibility

**Issue**: "Unauthorized" or 401 responses on API calls
**Solutions**:
- Ensure JWT token is included in Authorization header: `Authorization: Bearer <token>`
- Check token expiration (default 60 minutes)
- Verify user has required role or policy for the endpoint
- Check JWT configuration in `appsettings.json` (Secret, Issuer, Audience)

**Issue**: "Assembly not found" or dependency errors
**Solutions**:
- Run `dotnet restore` in solution root
- Clear NuGet cache: `dotnet nuget locals all --clear`
- Rebuild solution: `dotnet build`
- Check package compatibility with .NET 10

**Issue**: Port already in use (5000/5001)
**Solutions**:
- Change port in `launchSettings.json` (Properties folder)
- Kill existing process: `netstat -ano | findstr :5001` then `taskkill /PID <pid> /F`
- Use different port: `dotnet run --urls "https://localhost:5002"`

**Issue**: EF Core migrations fail
**Solutions**:
- Ensure Infrastructure project references Application and Domain
- Check DbContext is properly registered in DI
- Verify connection string is accessible
- Run with verbose logging: `dotnet ef database update --verbose`

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
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "{Project Name} API",
        Version = "v1",
        Description = "API for managing {domain}",
        Contact = new OpenApiContact
        {
            Name = "Development Team",
            Email = "dev@example.com",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    // Add JWT authentication to Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    // Include XML comments (optional)
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

// Middleware configuration
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "{Project Name} API V1");
        options.RoutePrefix = string.Empty;  // Serve Swagger UI at application root
        options.DocumentTitle = "{Project Name} API Documentation";
        options.DisplayRequestDuration();
        options.EnableDeepLinking();
        options.EnableFilter();
    });
}
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
