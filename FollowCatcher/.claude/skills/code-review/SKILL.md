---
name: code-review
description: Perform comprehensive code reviews with actionable feedback. Use when reviewing pull requests, checking code quality, analyzing changes, or when the user mentions "review", "PR", "pull request", "check code", "code quality".
allowed-tools: Read, Grep, Glob
---

# Code Review Guide

**Project Requirements**: .NET 10, C# 14

## Review Process

When reviewing code, analyze the following aspects systematically:

### 1. Correctness
- Does the code do what it's supposed to do?
- Are edge cases handled?
- Are there any logical errors?
- Are null/undefined cases handled?

### 2. Security

#### ASP.NET Core Security
- [ ] CORS policy configured (not AllowAnyOrigin in production)
- [ ] Security headers present (X-Frame-Options, X-Content-Type-Options, CSP)
- [ ] HTTPS enforced (UseHttpsRedirection, HSTS)
- [ ] No sensitive data in logs, exceptions, or error messages
- [ ] Dependencies checked for vulnerabilities (`dotnet list package --vulnerable`)
- [ ] Rate limiting configured for public endpoints
- [ ] Request size limits configured (prevent DoS)
- [ ] Authentication/Authorization on all endpoints (explicit [AllowAnonymous])

#### Input Validation
- [ ] FluentValidation integrated with MediatR pipeline
- [ ] Model binding validation enabled ([ApiController] attribute)
- [ ] File upload limits and type validation
- [ ] No user input directly in SQL (EF Core parameterizes automatically)
- [ ] Command injection prevention (avoid Process.Start with user input)

#### Data Protection
- [ ] Sensitive data encrypted at rest (IDataProtectionProvider)
- [ ] PII data not logged or exposed in error messages
- [ ] Secrets in configuration (Azure Key Vault, User Secrets, not appsettings.json)
- [ ] SQL injection prevented (use EF Core, never string concatenation)
- [ ] XSS prevented (ASP.NET Core auto-encodes, APIs return JSON)
- [ ] CSRF protection for state-changing operations
- [ ] Password hashing with proper algorithms (Identity, BCrypt, Argon2)

### 3. Performance
- [ ] No N+1 query problems
- [ ] Appropriate use of async/await
- [ ] No unnecessary database calls
- [ ] Proper indexing considerations
- [ ] No memory leaks
- [ ] Efficient algorithms

### 4. Maintainability
- [ ] Clear naming conventions
- [ ] Single Responsibility Principle
- [ ] DRY (Don't Repeat Yourself)
- [ ] Appropriate comments for complex logic
- [ ] Consistent code style

### 5. Architecture Compliance
- [ ] Follows project layer structure
- [ ] Dependencies point inward
- [ ] Domain logic in Domain layer
- [ ] No business logic in Controllers
- [ ] Proper use of DTOs

### 6. CQRS Compliance
- [ ] Commands mutate state, Queries don't
- [ ] All commands/queries implement IRequest<TResponse>
- [ ] Handlers implement IRequestHandler<TRequest, TResponse>
- [ ] Pipeline behaviors registered (Validation, Logging, Performance)
- [ ] MediatR properly configured in DI
- [ ] No business logic in controllers (only in handlers)
- [ ] Queries use AsNoTracking() for read-only operations
- [ ] Commands validate input through FluentValidation
- [ ] Handlers have single responsibility
- [ ] DTOs separate from domain entities

## Review Output Format

```markdown
## Code Review Summary

### ✅ What's Good
- [List positive aspects]

### ⚠️ Suggestions
- [Non-blocking improvements]

### ❌ Issues (Must Fix)
- [Critical issues that must be addressed]

### 📋 Checklist
- [ ] Security review passed
- [ ] Performance review passed
- [ ] Tests included
- [ ] Documentation updated
```

## Common Issues to Watch For

### .NET Specific

#### Async/Await Best Practices

**❌ Bad Patterns:**
```csharp
// 🟠 Fire and forget - exceptions are lost
public void DoSomething()
{
    service.SaveAsync();  // Missing await
}

// 🔴 Blocking async code - deadlock risk
public void ProcessData()
{
    var result = asyncMethod().Result;  // Deadlock!
    var result2 = asyncMethod().GetAwaiter().GetResult();  // Still blocking!
}

// 🔴 Async void - can't catch exceptions (except event handlers)
public async void ProcessData()
{
    await service.SaveAsync();  // Exception crashes app
}

// 🟡 Missing ConfigureAwait in libraries
public async Task<Data> GetDataAsync()
{
    await service.CallAsync();  // Captures context unnecessarily
    return data;
}

// 🟠 Not propagating CancellationToken
public async Task ProcessAsync()
{
    await service.LongRunningOperationAsync();  // Missing ct parameter
}
```

**✅ Good Patterns:**
```csharp
// Async all the way
public async Task DoSomethingAsync(CancellationToken ct)
{
    await this.service.SaveAsync(ct);
}

// ConfigureAwait in library code
public async Task<Data> GetDataAsync(CancellationToken ct)
{
    await this.service.CallAsync(ct).ConfigureAwait(false);
    return data;
}

// ValueTask for frequently-awaited hot paths
public ValueTask<int> GetCachedValueAsync(string key)
{
    if (this.cache.TryGetValue(key, out var value))
        return new ValueTask<int>(value);  // Synchronous completion

    return new ValueTask<int>(LoadFromDbAsync(key));  // Async path
}

// Proper CancellationToken propagation
public async Task ProcessAsync(CancellationToken ct)
{
    ct.ThrowIfCancellationRequested();
    await this.service.ProcessAsync(ct);
}

// Async event handler (only valid async void usage)
private async void OnButtonClick(object sender, EventArgs e)
{
    try
    {
        await this.ProcessDataAsync();
    }
    catch (Exception ex)
    {
        this.logger.LogError(ex, "Error in event handler");
    }
}
```

**Checklist:**
- [ ] All async methods properly awaited
- [ ] No async void (except event handlers with try-catch)
- [ ] ConfigureAwait(false) in library code
- [ ] No .Result/.Wait() calls (deadlock risk)
- [ ] ValueTask used for hot paths when appropriate
- [ ] CancellationToken propagated through entire chain
- [ ] Task.WhenAll for parallel operations
- [ ] Async methods named with Async suffix

#### EF Core Query Best Practices

**❌ Bad Patterns:**
```csharp
// 🔴 N+1 query - database hit per iteration
foreach (var order in orders)
{
    var customer = await this.context.Customers.FindAsync(order.CustomerId);
}

// 🟠 Loading everything from database
var employees = this.context.Employees.ToList();  // Loads all columns, all rows

// 🔴 Lazy loading enabled - hidden N+1 queries
services.AddDbContext<AppDbContext>(options =>
    options.UseLazyLoadingProxies());  // Performance killer

// 🟠 Change tracking for read-only queries
var employees = await this.context.Employees
    .Where(e => e.IsActive)
    .ToListAsync();  // Unnecessary tracking overhead

// 🟡 Exposing domain entities in API
[HttpGet]
public async Task<Employee> Get(Guid id) => await this.repository.GetAsync(id);

// 🟡 Multiple database roundtrips
await this.context.Employees.Where(e => e.DepartmentId == id).ExecuteDeleteAsync();
await this.context.SaveChangesAsync();  // Already executed
```

**✅ Good Patterns:**
```csharp
// Eager loading - single query with JOIN
var orders = await this.context.Orders
    .Include(o => o.Customer)
    .ThenInclude(c => c.Address)
    .ToListAsync(ct);

// Projection with AsNoTracking for read-only
var dtos = await this.context.Employees
    .AsNoTracking()
    .Where(e => e.IsActive)
    .Select(e => new EmployeeDto(e.Id, e.Name, e.Email))
    .ToListAsync(ct);

// Batch operations (EF Core 7+)
await this.context.Employees
    .Where(e => e.DepartmentId == departmentId)
    .ExecuteUpdateAsync(
        setters => setters.SetProperty(e => e.IsActive, false),
        ct);

// Split query for multiple collections (avoid cartesian explosion)
var employees = await this.context.Employees
    .Include(e => e.Skills)
    .Include(e => e.Certifications)
    .AsSplitQuery()
    .ToListAsync(ct);

// Compiled queries for frequently-executed queries
private static readonly Func<AppDbContext, Guid, Task<Employee?>> GetEmployeeById =
    EF.CompileAsyncQuery((AppDbContext context, Guid id) =>
        context.Employees.FirstOrDefault(e => e.Id == id));

public async Task<Employee?> GetByIdAsync(Guid id, CancellationToken ct)
{
    return await GetEmployeeById(this.context, id);
}

// Using DTOs to prevent over-fetching
[HttpGet]
public async Task<EmployeeDto> Get(Guid id, CancellationToken ct)
{
    var dto = await this.context.Employees
        .AsNoTracking()
        .Where(e => e.Id == id)
        .Select(e => new EmployeeDto(e.Id, e.Name, e.Email))
        .FirstOrDefaultAsync(ct);

    return dto ?? throw new NotFoundException(nameof(Employee), id);
}
```

**Checklist:**
- [ ] No N+1 queries (enable logging to detect: `.EnableSensitiveDataLogging()`)
- [ ] .Include() / .ThenInclude() for needed relationships
- [ ] AsNoTracking() for read-only queries
- [ ] Projections (Select) used instead of loading full entities
- [ ] Batch operations (ExecuteUpdate/ExecuteDelete) for bulk changes
- [ ] AsSplitQuery() for multiple collections (avoid cartesian explosion)
- [ ] Proper indexes on frequently queried/filtered columns
- [ ] Compiled queries for hot paths

#### Structured Logging Best Practices

**❌ Bad Patterns:**
```csharp
// 🟡 String interpolation - not structured, performance cost
logger.LogError($"Error processing user {userId}: {ex.Message}");

// 🟡 Concatenation - same issues
logger.LogInformation("Processing " + orderId + " for user " + userId);

// 🔴 Logging sensitive data
logger.LogInformation($"User password: {password}");  // PII violation
logger.LogDebug($"Credit card: {creditCard}");  // Security violation

// 🟠 Missing exception parameter
logger.LogError("Database error: " + ex.Message);  // Loses stack trace

// 🟡 Wrong log levels
logger.LogInformation("CRITICAL FAILURE!");  // Should be LogCritical
logger.LogError("User clicked button");  // Should be LogDebug/LogTrace
```

**✅ Good Patterns:**
```csharp
// Structured logging with parameters
logger.LogInformation("Processing order {OrderId} for user {UserId}", orderId, userId);

// Exception with message template
logger.LogError(ex, "Error processing user {UserId}", userId);

// Appropriate log levels
logger.LogTrace("Entering method GetEmployeeById with id {EmployeeId}", id);
logger.LogDebug("Cache miss for key {CacheKey}", key);
logger.LogInformation("Employee {EmployeeId} created successfully", employeeId);
logger.LogWarning("Retry attempt {Attempt} for operation {Operation}", attempt, operation);
logger.LogError(ex, "Failed to process order {OrderId}", orderId);
logger.LogCritical(ex, "Database connection lost");

// High-performance logging with LoggerMessage
private static readonly Action<ILogger, Guid, Exception?> LogEmployeeCreated =
    LoggerMessage.Define<Guid>(
        LogLevel.Information,
        new EventId(1, nameof(LogEmployeeCreated)),
        "Employee created with ID {EmployeeId}");

public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken ct)
{
    var employee = new Employee(request.Name);
    this.context.Employees.Add(employee);
    await this.context.SaveChangesAsync(ct);

    LogEmployeeCreated(this.logger, employee.Id, null);
    return employee.Id;
}

// Correlation IDs for request tracking
using (this.logger.BeginScope(new Dictionary<string, object>
{
    ["CorrelationId"] = HttpContext.TraceIdentifier,
    ["UserId"] = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
}))
{
    logger.LogInformation("Processing request");
}
```

**Checklist:**
- [ ] Structured logging with ILogger<T> (not string interpolation)
- [ ] Correlation IDs for distributed tracing
- [ ] No sensitive data logged (passwords, tokens, PII, credit cards)
- [ ] Appropriate log levels (Trace < Debug < Information < Warning < Error < Critical)
- [ ] Exception parameter passed to Log methods
- [ ] LoggerMessage for high-performance hot paths
- [ ] Consistent message templates across similar operations

### General Issues
```csharp
// ❌ 🟡 Bad: Magic numbers
if (status == 3) { }

// ✅ Good: Named constants or enums
if (status == OrderStatus.Completed) { }
```

```csharp
// ❌ 🟠 Bad: Catching all exceptions
try { } catch (Exception) { }

// ✅ Good: Specific exception handling
try { } catch (DbUpdateException ex) { this.logger.LogError(ex, "..."); }
```

## Severity Levels

| Level | Description | Action |
|-------|-------------|--------|
| 🔴 Critical | Security, data loss risk | Block merge |
| 🟠 Major | Bugs, performance issues | Should fix |
| 🟡 Minor | Code style, naming | Nice to fix |
| 🟢 Nitpick | Preferences | Optional |

## Review Etiquette

1. Be constructive, not critical
2. Explain the "why" behind suggestions
3. Offer solutions, not just problems
4. Praise good code
5. Ask questions instead of making demands
