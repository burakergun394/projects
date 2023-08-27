namespace Application.Exceptions;

internal class ValueNotFoundException : BusinessException
{
    public ValueNotFoundException(string message) : base(message: message)
    {
    }
}
