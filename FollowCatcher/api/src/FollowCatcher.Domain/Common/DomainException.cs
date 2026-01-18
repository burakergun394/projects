namespace FollowCatcher.Domain.Common;

/// <summary>
/// Base class for domain exceptions.
/// Domain exceptions represent violations of business rules or invariants.
/// </summary>
public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message)
    {
    }

    protected DomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

/// <summary>
/// Exception thrown when a requested entity is not found.
/// </summary>
public class NotFoundException : DomainException
{
    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string entityName, object key)
        : base($"{entityName} with ID '{key}' was not found.")
    {
    }
}

/// <summary>
/// Exception thrown when a business rule validation fails.
/// </summary>
public class BusinessRuleValidationException : DomainException
{
    public BusinessRuleValidationException(string message) : base(message)
    {
    }
}

/// <summary>
/// Exception thrown when an entity already exists.
/// </summary>
public class DuplicateException : DomainException
{
    public DuplicateException(string message) : base(message)
    {
    }

    public DuplicateException(string entityName, object key)
        : base($"{entityName} with ID '{key}' already exists.")
    {
    }
}
