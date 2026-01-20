# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build & Run Commands

```bash
# Build the solution
cd api && dotnet build

# Run the API (from api/src/FollowCatcher.Api)
dotnet run

# Run with watch mode for development
dotnet watch run

# Database migrations (from api/src/FollowCatcher.Persistence)
dotnet ef migrations add <MigrationName> --startup-project ../FollowCatcher.Api
dotnet ef database update --startup-project ../FollowCatcher.Api

# Access Swagger UI after running: https://localhost:<port>/swagger
```

## Architecture Overview

This is a .NET 10/C# 14 Clean Architecture project with CQRS pattern for monitoring Instagram accounts and posting notifications to Twitter.

```
FollowCatcher.Api          → Presentation (Controllers, Program.cs)
       ↓
FollowCatcher.Application  → Use Cases (Commands, Queries, Handlers, DTOs, BackgroundServices)
       ↓
FollowCatcher.Domain       → Core (Entities, Events, Interfaces) - NO external dependencies
       ↑
FollowCatcher.Persistence  → Data Access (DbContext, Repositories, EF Configurations)
FollowCatcher.Infrastructure → External Services (Instagram API, Twitter API, Image Generation)
```

**Key Patterns:**
- **CQRS** via MediatR: Commands/Queries in `Application/{Feature}/Commands|Queries/`
- **Domain Events**: Published on SaveChanges via MediatR (e.g., `UserFollowedEvent`, `UserUnfollowedEvent`)
- **Repository Pattern**: Interfaces in Domain, implementations in Persistence
- **Unit of Work**: `ApplicationDbContext` implements `IUnitOfWork`
- **Soft Delete**: Entities use `DeletedAt` with query filters

## Code Conventions

**Field naming**: Use `this.field` pattern, NOT underscore prefix (`_field`)

**GUIDs**: Use `Guid.CreateVersion7()` (time-ordered) instead of `Guid.NewGuid()`

**Primary constructors** (C# 14):
```csharp
public class CreateEmployeeHandler(IApplicationDbContext context) : IRequestHandler<CreateEmployeeCommand, Guid>
```

**Queries must use** `AsNoTracking()` and return DTOs, never domain entities.

## Project Structure for New Features

1. **Domain Entity**: `Domain/{Feature}/{Feature}.cs` - Inherit from `Entity` base class
2. **Domain Events**: `Domain/{Feature}/{Feature}Events.cs` - Raise via `RaiseDomainEvent()`
3. **Commands**: `Application/{Feature}/Commands/{Action}/` with Command.cs + Handler.cs
4. **Queries**: `Application/{Feature}/Queries/{Action}/` with Query.cs + Handler.cs
5. **DTOs**: `Application/{Feature}/Dtos/{Feature}Dto.cs`
6. **Controller**: `Api/Controllers/{Features}Controller.cs` - Inject `IMediator`
7. **EF Config**: `Persistence/Configurations/{Feature}Configuration.cs`
8. **Repository**: Interface in Domain, implementation in Persistence

## Configuration

Sensitive credentials go in `appsettings.Local.json` (git-ignored):
- Instagram: `Username`, `Password` in `InstagramSettings`
- Twitter: `ApiKey`, `ApiKeySecret`, `AccessToken`, `AccessTokenSecret` in `TwitterSettings`

## Key Files

- **Entry Point**: `api/src/FollowCatcher.Api/Program.cs`
- **DbContext**: `api/src/FollowCatcher.Persistence/ApplicationDbContext.cs`
- **Base Entity**: `api/src/FollowCatcher.Domain/Common/Entity.cs`
- **Instagram Service**: `api/src/FollowCatcher.Infrastructure/Instagram/InstagramService.cs`
- **Twitter Service**: `api/src/FollowCatcher.Infrastructure/Twitter/TwitterService.cs`
- **Background Worker**: `api/src/FollowCatcher.Application/Instagram/BackgroundServices/InstagramTrackedAccountBackgroundService.cs`

## Local Skills

Project-specific Claude skills are in `.claude/skills/`:
- `/api-endpoint` - Create complete API endpoints
- `/clean-architecture` - Architecture patterns guide
- `/domain-entity` - Create domain entities with DDD
- `/code-review` - Perform code reviews
- `/documentation` - Generate documentation
