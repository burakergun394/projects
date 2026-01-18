---
name: documentation
description: Generate code documentation, API docs, and technical guides. Use when documenting code, creating API documentation, writing technical specs, or when the user mentions "document", "docs", "README", "API docs", "swagger".
allowed-tools: Read, Grep, Glob, Write, Edit
---

# Documentation Guide

> **Project Requirements**: 
> - **NO XML Documentation** (`/// <summary>`) allowed in code.
> - **NO** inline comments explaining "what" code does.
> - **ONLY** use `// TODO: ...` for future work or critical "WHY" comments.
> - For detailed rules, refer to **[.agent/skills/project-standards.md](file:///c:/Users/Burak/Documents/MY/GITHUB/BURAKERGUN394/projects/FollowCatcher/.agent/skills/project-standards.md)**.

## Inline Comment Philosophy

**Core Principle**: Comments should explain **WHY**, not **WHAT**. Code should be self-documenting through clear naming and structure.

### Accepted Comment Types

1.  **TODOs**:
    ```csharp
    // TODO(JIRA-123): Refactor this to use the new API client
    ```

2.  **Critical Explanations (The WHY)**:
    ```csharp
    // We normalize to lowercase because the legacy database collation is case-sensitive
    Email = value.ToLowerInvariant();
    ```

### Forbidden Comment Types

1.  **XML Documentation**:
    ```csharp
    // ❌ FORBIDDEN
    /// <summary>
    /// Gets the user by ID.
    /// </summary>
    public User GetById(Guid id)
    ```

2.  **Explaining the WHAT**:
    ```csharp
    // ❌ FORBIDDEN
    // Loop through users
    foreach (var user in users)
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
- SQL Server
- Docker

## Getting Started

### Installation

```bash
git clone https://github.com/username/project.git
cd project
dotnet restore
```

## API Documentation

Swagger UI is available at `/swagger` when running in Development mode.

## Environment Variables

Create a `appsettings.Local.json` file:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=YourDb;User Id=sa;Password=YourPassword;TrustServerCertificate=True"
  }
}
```
```

## Checklist

- [x] Check code for forbidden XML documentation (`///`)
- [x] Ensure all comments explain "WHY" or are "TODOs"
- [ ] Verify README is up to date
