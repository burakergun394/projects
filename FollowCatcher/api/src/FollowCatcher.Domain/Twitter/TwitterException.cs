namespace FollowCatcher.Domain.Twitter;

public class TwitterException : Exception
{
    public TwitterErrorCode ErrorCode { get; }

    public TwitterException(TwitterErrorCode errorCode, string message)
        : base(message)
    {
        ErrorCode = errorCode;
    }

    public TwitterException(TwitterErrorCode errorCode, string message, Exception innerException)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
    }
}

public enum TwitterErrorCode
{
    ImageUploadFailed,
    TweetPublishFailed,
    InvalidResponse,
    RateLimitExceeded,
    Unauthorized
}
