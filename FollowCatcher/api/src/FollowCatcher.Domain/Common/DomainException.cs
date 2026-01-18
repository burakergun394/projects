namespace FollowCatcher.Domain.Common;


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


public class BusinessRuleValidationException : DomainException
{
    public BusinessRuleValidationException(string message) : base(message)
    {
    }
}


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
