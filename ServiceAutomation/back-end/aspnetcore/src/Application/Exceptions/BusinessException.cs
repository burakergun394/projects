namespace Application.Exceptions;

internal class BusinessException : Exception
{
    public BusinessException(string message) : base(message: message)
    {

    }
}