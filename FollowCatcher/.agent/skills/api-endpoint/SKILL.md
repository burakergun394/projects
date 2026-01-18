---
name: api-endpoint
description: Create complete API endpoints following Clean Architecture. Use when the user wants to add a new endpoint, create an API, implement CRUD operations, or mentions "endpoint", "controller", "API", "REST".
allowed-tools: Read, Grep, Glob, Write, Edit
---

# API Endpoint Creation Guide

**Project Requirements**: .NET 10, C# 14

## Placeholder Legend

This guide uses the following placeholders that should be replaced with your specific values:
- `{ProjectName}`: Your project name (e.g., `FollowCatcher`)
- `{Feature}`: Singular entity/feature name (e.g., `Employee`, `Vehicle`, `Route`)
- `{Features}`: Plural entity/feature name (e.g., `Employees`, `Vehicles`, `Routes`)
- `{Action}`: Action name (e.g., `Create`, `Update`, `Delete`, `Get`)

## Coding Conventions

### Field Naming Convention
This project uses the `this.field` pattern for private fields. Do NOT use underscore prefix.

```csharp
// ✅ Good: Using this.field pattern
public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, Guid>
{
    private readonly IApplicationDbContext context;

    public CreateEmployeeHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken ct)
    {
        var employee = new Employee(request.Name);
        this.context.Employees.Add(employee);
        await this.context.SaveChangesAsync(ct);
        return employee.Id;
    }
}

// ❌ Bad: Using underscore prefix
public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, Guid>
{
    private readonly IApplicationDbContext _context;  // Don't use underscore

    public CreateEmployeeHandler(IApplicationDbContext context)
    {
        _context = context;  // Don't use underscore
    }
}
```

## Endpoint Creation Workflow

When creating a new API endpoint, follow this complete workflow:

### Step 1: Create Command/Query in Application Layer

**For Write Operations (POST, PUT, DELETE):**
```csharp
// Location: Application/{Feature}/Commands/{Action}/{Action}{Feature}Command.cs
public record Create{Feature}Command(
    string Property1,
    string Property2
) : IRequest<Guid>;

// Location: Application/{Feature}/Commands/{Action}/{Action}{Feature}Handler.cs
// C# 14 primary constructor - no need for field declaration or constructor
public class Create{Feature}Handler(IApplicationDbContext context)
    : IRequestHandler<Create{Feature}Command, Guid>
{
    public async Task<Guid> Handle(Create{Feature}Command request, CancellationToken cancellationToken)
    {
        var entity = new {Feature}(request.Property1, request.Property2);

        context.{Features}.Add(entity);
        await context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
```

**For Read Operations (GET):**
```csharp
// Location: Application/{Feature}/Queries/{Action}/{Action}Query.cs
public record Get{Feature}ByIdQuery(Guid Id) : IRequest<{Feature}Dto>;

// Location: Application/{Feature}/Queries/{Action}/{Action}Handler.cs
// C# 14 primary constructor - cleaner and more concise
public class Get{Feature}ByIdHandler(IApplicationDbContext context)
    : IRequestHandler<Get{Feature}ByIdQuery, {Feature}Dto>
{
    public async Task<{Feature}Dto> Handle(Get{Feature}ByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await context.{Features}
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return entity is null ? null : new {Feature}Dto(entity.Id, entity.Property1);
    }
}
```

### Step 2: Create DTO

```csharp
// Location: Application/{Feature}/Queries/{Feature}Dto.cs
public record {Feature}Dto(
    Guid Id,
    string Property1,
    string Property2
);
```

### Step 3: Create Controller

```csharp
// Location: Api/Controllers/{Features}Controller.cs
[ApiController]
[Route("api/[controller]")]
[Authorize]
[Produces("application/json")]
public class {Features}Controller(IMediator mediator) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<{Feature}Dto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResult<{Feature}Dto>>> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new GetAll{Features}Query(pageNumber, pageSize);
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof({Feature}Dto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<{Feature}Dto>> GetById(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new Get{Feature}ByIdQuery(id), cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [Authorize(Policy = "CanCreate{Features}")]
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> Create(
        [FromBody] Create{Feature}Command command,
        CancellationToken cancellationToken = default)
    {
        var id = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [Authorize(Policy = "CanUpdate{Features}")]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] Update{Feature}Command command,
        [FromHeader(Name = "If-Match")] string? etag,
        CancellationToken cancellationToken = default)
    {
        if (id != command.Id)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "ID Mismatch",
                Detail = "The ID in the URL does not match the ID in the request body"
            });
        }

        try
        {
            await mediator.Send(command with { ETag = etag }, cancellationToken);
            return NoContent();
        }
        catch (ConcurrencyException)
        {
            return Conflict(new ProblemDetails
            {
                Title = "Concurrency Conflict",
                Detail = "The resource was modified by another user"
            });
        }
    }

    [Authorize(Policy = "CanDelete{Features}")]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        await mediator.Send(new Delete{Feature}Command(id), cancellationToken);
        return NoContent();
    }
}
```

## RESTful Conventions

| HTTP Method | Action | Route | Returns |
|-------------|--------|-------|---------|
| GET | Get all | `/api/features` | List<Dto> |
| GET | Get by ID | `/api/features/{id}` | Dto |
| POST | Create | `/api/features` | Created ID |
| PUT | Update | `/api/features/{id}` | NoContent |
| DELETE | Delete | `/api/features/{id}` | NoContent |

## Validation

Add FluentValidation for commands:

```csharp
// Location: Application/{Feature}/Commands/{Action}/{Action}{Feature}Validator.cs
public class Create{Feature}Validator : AbstractValidator<Create{Feature}Command>
{
    public Create{Feature}Validator()
    {
        RuleFor(x => x.Property1)
            .NotEmpty().WithMessage("Property1 is required")
            .MaximumLength(100).WithMessage("Property1 must not exceed 100 characters");
    }
}
```

## Error Handling with ProblemDetails

Use standardized error responses with ProblemDetails for consistent API error handling.

```csharp
// Install: Microsoft.AspNetCore.Mvc.ProblemDetails

// Location: Api/Middleware/ExceptionHandlingMiddleware.cs
public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            logger.LogWarning(ex, "Validation error occurred");

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "Validation Error",
                Status = StatusCodes.Status400BadRequest,
                Detail = string.Join(", ", ex.Errors.Select(e => e.ErrorMessage)),
                Instance = context.Request.Path
            });
        }
        catch (NotFoundException ex)
        {
            logger.LogWarning(ex, "Resource not found: {Message}", ex.Message);

            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "Resource Not Found",
                Status = StatusCodes.Status404NotFound,
                Detail = ex.Message,
                Instance = context.Request.Path
            });
        }
        catch (UnauthorizedAccessException ex)
        {
            logger.LogWarning(ex, "Unauthorized access attempt");

            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3",
                Title = "Forbidden",
                Status = StatusCodes.Status403Forbidden,
                Detail = "You do not have permission to access this resource",
                Instance = context.Request.Path
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unhandled exception occurred");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Title = "Internal Server Error",
                Status = StatusCodes.Status500InternalServerError,
                Detail = "An error occurred while processing your request",
                Instance = context.Request.Path
            });
        }
    }
}

// Custom exceptions
// Location: Application/Common/Exceptions/NotFoundException.cs
public class NotFoundException : Exception
{
    public NotFoundException(string name, object key)
        : base($"{name} with id '{key}' was not found")
    {
    }
}

// Location: Application/Common/Exceptions/ValidationException.cs
public class ValidationException : Exception
{
    public IEnumerable<ValidationFailure> Errors { get; }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : base("One or more validation failures occurred")
    {
        Errors = failures;
    }
}

// Registration in Program.cs
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Alternative: Use built-in ProblemDetails support (.NET 7+)
builder.Services.AddProblemDetails();
app.UseExceptionHandler();
app.UseStatusCodePages();
```

## MediatR Pipeline Behaviors

Pipeline behaviors allow you to add cross-cutting concerns like validation, logging, and performance monitoring.

```csharp
// Location: Application/Common/Behaviors/ValidationBehavior.cs
public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Count != 0)
        {
            throw new ValidationException(failures);
        }

        return await next();
    }
}

// Location: Application/Common/Behaviors/LoggingBehavior.cs
public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        logger.LogInformation("Handling {RequestName}", requestName);

        var response = await next();

        logger.LogInformation("Handled {RequestName}", requestName);

        return response;
    }
}

// Location: Application/Common/Behaviors/PerformanceBehavior.cs
public class PerformanceBehavior<TRequest, TResponse>(
    ILogger<PerformanceBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();

        var response = await next();

        stopwatch.Stop();

        var requestName = typeof(TRequest).Name;
        var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

        if (elapsedMilliseconds > 500)
        {
            logger.LogWarning(
                "Long Running Request: {RequestName} ({ElapsedMilliseconds} ms)",
                requestName,
                elapsedMilliseconds);
        }

        return response;
    }
}

// Registration in Program.cs (Application layer registration)
// Location: Application/DependencyInjection.cs
public static IServiceCollection AddApplication(this IServiceCollection services)
{
    services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
        cfg.AddOpenBehavior(typeof(PerformanceBehavior<,>));
    });

    services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

    return services;
}
```

## Pagination Pattern

Implement pagination for list endpoints to improve performance and user experience.

```csharp
// Location: Application/Common/Models/PagedResult.cs
public record PagedResult<T>(
    List<T> Items,
    int TotalCount,
    int PageNumber,
    int PageSize)
{
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPrevious => PageNumber > 1;
    public bool HasNext => PageNumber < TotalPages;
}

// Location: Application/{Feature}/Queries/GetAll{Features}Query.cs
public record GetAll{Features}Query(
    int PageNumber = 1,
    int PageSize = 10) : IRequest<PagedResult<{Feature}Dto>>;

// Location: Application/{Feature}/Queries/GetAll{Features}Handler.cs
public class GetAll{Features}Handler(IApplicationDbContext context)
    : IRequestHandler<GetAll{Features}Query, PagedResult<{Feature}Dto>>
{
    public async Task<PagedResult<{Feature}Dto>> Handle(
        GetAll{Features}Query request,
        CancellationToken cancellationToken)
    {
        var query = context.{Features}.AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new {Feature}Dto(x.Id, x.Property1, x.Property2))
            .ToListAsync(cancellationToken);

        return new PagedResult<{Feature}Dto>(
            items,
            totalCount,
            request.PageNumber,
            request.PageSize);
    }
}

// Extension method for easier pagination (optional)
// Location: Application/Common/Extensions/QueryableExtensions.cs
public static class QueryableExtensions
{
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<T>(items, totalCount, pageNumber, pageSize);
    }
}
```

## Idempotency Pattern

Ensure critical operations (payments, orders) can be safely retried without side effects.

```csharp
// Location: Application/Common/Interfaces/IIdempotencyCache.cs
public interface IIdempotencyCache
{
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);
    Task SetAsync<T>(string key, T value, TimeSpan expiration, CancellationToken cancellationToken = default);
}

// Location: Infrastructure/Services/IdempotencyCache.cs
public class IdempotencyCache(IDistributedCache cache) : IIdempotencyCache
{
    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var data = await cache.GetStringAsync(key, cancellationToken);
        return data is null ? default : JsonSerializer.Deserialize<T>(data);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan expiration, CancellationToken cancellationToken = default)
    {
        var data = JsonSerializer.Serialize(value);
        var options = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = expiration };
        await cache.SetStringAsync(key, data, options, cancellationToken);
    }
}

// Controller example with idempotency
[HttpPost("process-payment")]
[ProducesResponseType(typeof(PaymentResult), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
public async Task<ActionResult<PaymentResult>> ProcessPayment(
    [FromBody] ProcessPaymentCommand command,
    [FromHeader(Name = "Idempotency-Key")] string? idempotencyKey,
    CancellationToken cancellationToken = default)
{
    if (string.IsNullOrWhiteSpace(idempotencyKey))
    {
        return BadRequest(new ProblemDetails
        {
            Title = "Idempotency Key Required",
            Detail = "The Idempotency-Key header is required for payment operations"
        });
    }

    // Check if this request was already processed
    var cachedResult = await idempotencyCache.GetAsync<PaymentResult>(
        idempotencyKey,
        cancellationToken);

    if (cachedResult is not null)
    {
        return Ok(cachedResult);  // Return cached result
    }

    // Process the payment
    var result = await mediator.Send(command, cancellationToken);

    // Cache the result for 24 hours
    await idempotencyCache.SetAsync(
        idempotencyKey,
        result,
        TimeSpan.FromHours(24),
        cancellationToken);

    return Ok(result);
}

// Registration in Program.cs
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddScoped<IIdempotencyCache, IdempotencyCache>();
```

## Checklist for New Endpoint

- [ ] Create Command/Query record
- [ ] Create Handler class
- [ ] Create DTO if needed
- [ ] Create Validator if needed
- [ ] Add Controller action
- [ ] Add proper route attributes
- [ ] Add proper response types
- [ ] Test endpoint with actual HTTP request
