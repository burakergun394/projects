namespace Application.Exceptions;

internal class NotFoundException : BusinessException
{
    public NotFoundException(string message) : base(message: message)
    {
    }
}
