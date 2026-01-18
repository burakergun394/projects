namespace FollowCatcher.Domain.Instagram;


public interface IInstagramService
{
    Task<InstagramProfileInfo?> GetProfileInfoAsync(string username, CancellationToken cancellationToken = default);
}
