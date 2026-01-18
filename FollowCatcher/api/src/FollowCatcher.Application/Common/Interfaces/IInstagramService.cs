namespace FollowCatcher.Application.Common.Interfaces;


public interface IInstagramService
{

    Task<InstagramProfileInfo?> GetProfileInfoAsync(string username, CancellationToken cancellationToken = default);
}


public record InstagramProfileInfo(
    string Username,
    long FollowerCount,
    long FollowingCount,
    string ProfilePictureUrl,
    int PostCount
);
