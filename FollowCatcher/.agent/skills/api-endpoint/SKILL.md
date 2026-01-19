---
name: api-endpoint
description: Create complete API endpoints following Clean Architecture. Use when the user wants to add a new endpoint, create an API, implement CRUD operations, or mentions "endpoint", "controller", "API", "REST".
allowed-tools: Read, Grep, Glob, Write, Edit
---

# API Endpoint Creation Guide

> **Note**: This skill focuses on the *execution steps* ("The Recipe"). For theoretical concepts (CQRS, Validation, Error Handling), refer to **[.agent/skills/clean-architecture/SKILL.md](file:///c:/Users/Burak/Documents/MY/GITHUB/BURAKERGUN394/projects/FollowCatcher/.agent/skills/clean-architecture/SKILL.md)**.
> For shared rules (Constructors, .NET Version), refer to **[.agent/skills/project-standards.md](file:///c:/Users/Burak/Documents/MY/GITHUB/BURAKERGUN394/projects/FollowCatcher/.agent/skills/project-standards.md)**.

## Placeholder Legend

- `{ProjectName}`: Your project name (e.g., `FollowCatcher`)
- `{Feature}`: Singular entity/feature name (e.g., `Employee`, `Vehicle`)
- `{Features}`: Plural entity/feature name (e.g., `Employees`, `Vehicles`)
- `{Action}`: Action name (e.g., `Create`, `Update`, `Delete`, `Get`)

## Endpoint Creation Workflow

### Step 1: Create Command/Query in Application Layer

**For Write Operations (POST, PUT, DELETE):**
```csharp
// Location: Application/{Feature}/Commands/{Action}/{Action}{Feature}Command.cs
using Space.Abstraction.Contracts;

public record Create{Feature}Command(
    string Property1,
    string Property2
) : IRequest<Guid>;

// Location: Application/{Feature}/Commands/{Action}/{Action}{Feature}Handler.cs
using Space.Abstraction.Attributes;
using Space.Abstraction.Context;

public class Create{Feature}Handler(IApplicationDbContext context)
{
    [Handle]
    public async ValueTask<Guid> Handle(HandlerContext<Create{Feature}Command> ctx)
    {
        var request = ctx.Request;
        var entity = new {Feature}(request.Property1, request.Property2);

        context.{Features}.Add(entity);
        await context.SaveChangesAsync(ctx.CancellationToken);

        return entity.Id;
    }
}
```

**For Read Operations (GET):**
```csharp
// Location: Application/{Feature}/Queries/{Action}/{Action}Query.cs
using Space.Abstraction.Contracts;

public record Get{Feature}ByIdQuery(Guid Id) : IRequest<{Feature}Dto>;

// Location: Application/{Feature}/Queries/{Action}/{Action}Handler.cs
using Space.Abstraction.Attributes;
using Space.Abstraction.Context;

public class Get{Feature}ByIdHandler(IApplicationDbContext context)
{
    [Handle]
    public async ValueTask<{Feature}Dto> Handle(HandlerContext<Get{Feature}ByIdQuery> ctx)
    {
        var request = ctx.Request;
        var entity = await context.{Features}
            .FirstOrDefaultAsync(x => x.Id == request.Id, ctx.CancellationToken);

        if (entity is null)
            throw new NotFoundException($"{Feature} with ID {request.Id} not found");

        return new {Feature}Dto(entity.Id, entity.Property1);
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
using Space.Abstraction;

[ApiController]
[Route("api/[controller]")]
[Authorize]
[Produces("application/json")]
public class {Features}Controller(ISpace space) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<{Feature}Dto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResult<{Feature}Dto>>> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await space.Send<PagedResult<{Feature}Dto>>(
            new GetAll{Features}Query(pageNumber, pageSize), ct: cancellationToken);
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
        try
        {
            var result = await space.Send<{Feature}Dto>(
                new Get{Feature}ByIdQuery(id), ct: cancellationToken);
            return Ok(result);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [Authorize(Policy = "CanCreate{Features}")]
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> Create(
        [FromBody] Create{Feature}Command command,
        CancellationToken cancellationToken = default)
    {
        var id = await space.Send<Guid>(command, ct: cancellationToken);
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

        await space.Send<Unit>(command with { ETag = etag }, ct: cancellationToken);
        return NoContent();
    }

    [Authorize(Policy = "CanDelete{Features}")]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        await space.Send<Unit>(new Delete{Feature}Command(id), ct: cancellationToken);
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

## Checklist for New Endpoint

- [ ] Create Command/Query record
- [ ] Create Handler class
- [ ] Create DTO if needed
- [ ] Create Validator if needed (See `clean-architecture` for rules)
- [ ] Add Controller action
- [ ] Add proper route attributes
- [ ] Add proper response types
