namespace FollowCatcher.Application.Common.Interfaces;

/// <summary>
/// Service interface for Instagram operations.
/// </summary>
public interface IInstagramService
{
    /// <summary>
    /// Gets Instagram profile information for a given username.
    /// </summary>
    /// <param name="username">The Instagram username to fetch profile information for.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>Instagram profile information including follower count, following count, profile picture URL, and post count.</returns>
    Task<InstagramProfileInfo?> GetProfileInfoAsync(string username, CancellationToken cancellationToken = default);
}

/// <summary>
/// Instagram profile information.
/// </summary>
public record InstagramProfileInfo(
    string Username,
    long FollowerCount,
    long FollowingCount,
    string ProfilePictureUrl,
    int PostCount
);
