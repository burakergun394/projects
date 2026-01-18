namespace FollowCatcher.Application.Instagram.Queries;

/// <summary>
/// Data transfer object for Instagram profile information.
/// </summary>
public record InstagramProfileDto(
    string Username,
    long FollowerCount,
    long FollowingCount,
    string ProfilePictureUrl,
    int PostCount
);
