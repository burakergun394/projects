---
name: api-endpoint
description: Create complete API endpoints following Clean Architecture. Use when the user wants to add a new endpoint, create an API, implement CRUD operations, or mentions "endpoint", "controller", "API", "REST".
allowed-tools: Read, Grep, Glob, Write, Edit
---

# API Endpoint Creation Guide

**Project Requirements**: .NET 10, C# 14

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
public class Create{Feature}Handler : IRequestHandler<Create{Feature}Command, Guid>
{
    private readonly IApplicationDbContext context;

    public Create{Feature}Handler(IApplicationDbContext context)
    {
        this.context = context;
    }

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
public class Get{Feature}ByIdHandler : IRequestHandler<Get{Feature}ByIdQuery, {Feature}Dto>
{
    private readonly IApplicationDbContext context;

    public Get{Feature}ByIdHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

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
public class {Features}Controller : ControllerBase
{
    private readonly IMediator mediator;

    public {Features}Controller(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<{Feature}Dto>> GetById(Guid id)
    {
        var result = await mediator.Send(new Get{Feature}ByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] Create{Feature}Command command)
    {
        var id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Update{Feature}Command command)
    {
        if (id != command.Id) return BadRequest();
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await mediator.Send(new Delete{Feature}Command(id));
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

## Checklist for New Endpoint

- [ ] Create Command/Query record
- [ ] Create Handler class
- [ ] Create DTO if needed
- [ ] Create Validator if needed
- [ ] Add Controller action
- [ ] Add proper route attributes
- [ ] Add proper response types
- [ ] Test endpoint with actual HTTP request
