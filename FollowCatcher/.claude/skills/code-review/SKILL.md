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
- [ ] No hardcoded secrets or credentials
- [ ] Input validation present
- [ ] SQL injection prevention (parameterized queries)
- [ ] XSS prevention (output encoding)
- [ ] Authentication/Authorization checks
- [ ] Sensitive data not logged

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
```csharp
// ❌ Bad: Not awaiting async call
public void DoSomething()
{
    _service.SaveAsync();  // Fire and forget!
}

// ✅ Good: Properly awaited
public async Task DoSomethingAsync()
{
    await _service.SaveAsync();
}
```

```csharp
// ❌ Bad: N+1 query
foreach (var order in orders)
{
    var customer = await _context.Customers.FindAsync(order.CustomerId);
}

// ✅ Good: Eager loading
var orders = await _context.Orders
    .Include(o => o.Customer)
    .ToListAsync();
```

```csharp
// ❌ Bad: Exposing domain entity
[HttpGet]
public async Task<Employee> Get(Guid id) => await _repo.GetAsync(id);

// ✅ Good: Using DTO
[HttpGet]
public async Task<EmployeeDto> Get(Guid id)
{
    var employee = await _repo.GetAsync(id);
    return new EmployeeDto(employee.Id, employee.Name);
}
```

### General Issues
```csharp
// ❌ Bad: Magic numbers
if (status == 3) { }

// ✅ Good: Named constants or enums
if (status == OrderStatus.Completed) { }
```

```csharp
// ❌ Bad: Catching all exceptions
try { } catch (Exception) { }

// ✅ Good: Specific exception handling
try { } catch (DbUpdateException ex) { _logger.LogError(ex, "..."); }
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
