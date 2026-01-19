namespace FollowCatcher.Domain.Twitter;

public interface ITwitterService
{
    Task<long> UploadImageAsync(byte[] imageBytes, CancellationToken cancellationToken = default);

    Task<string> PublishTweetAsync(string text, long? mediaId = null, CancellationToken cancellationToken = default);
}
