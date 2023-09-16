namespace Domain.Shared.Responses;

public class Response<T>
{
    public bool IsSuccess { get; private set; }
    public T? Data { get; private set; }
    public string Message { get; private set; }

    public static Response<T> Success(T data, string message) => new()
    {
        IsSuccess = true,
        Data = data,
        Message = message
    };

    public static Response<T> Success(T data) => Success(data, "");

    public static Response<T> Success(string message) => Success(default, message);

    public static Response<T> Failure(string message) => new()
    {
        IsSuccess = false,
        Data = default,
        Message = message
    };
}