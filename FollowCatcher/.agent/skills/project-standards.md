# Project Standards & Shared Conventions

> **Critical**: These standards apply to ALL code in this project. All other skills inherit these rules.

## Tech Stack
- **Framework**: .NET 10
- **Language**: C# 14
- **Nullable Reference Types**: Enabled (`<Nullable>enable</Nullable>`)
- **Implicit Usings**: Enabled (`<ImplicitUsings>enable</ImplicitUsings>`)

## Coding Conventions
### Constructors
- **ALWAYS** prefer C# 12+ Primary Constructors for classes and records.
- Avoid defining explicit constructors unless validation requiring a method body is strictly necessary (and cannot be done in a factory method).

### Code Style
- Use `var` when type is obvious.
- Use file-scoped namespaces (`namespace MyNamespace;`).
- Use async/await for all I/O operations.

### Documentation
- **NO** XML Documentation (`/// <summary>`).
- **NO** inline comments explaining "what" the code does.
- **ONLY** use `// TODO: ...` for future work.


## Architecture Guidelines
- **Clean Architecture**: Follow the dependency rule (Domain <- Application <- Infrastructure <- API).
- **CQRS**: Use MediatR for all Application logic (Commands/Queries).
