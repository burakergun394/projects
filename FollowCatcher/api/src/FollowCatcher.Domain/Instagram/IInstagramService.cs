namespace FollowCatcher.Domain.Instagram;


public interface IInstagramService
{
    Task<InstagramProfileInfo?> GetProfileInfoAsync(string username, CancellationToken cancellationToken = default);
    Task<List<InstagramProfileInfo>?> GetUserFollowingAsync(string username, CancellationToken cancellationToken = default);
}
