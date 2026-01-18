namespace FollowCatcher.Domain.Twitter;

public interface ITwitterService
{
    Task<long> UploadImageAsync(byte[] imageBytes);

    Task<string?> PublishTweetAsync(string text, long mediaId);
}
