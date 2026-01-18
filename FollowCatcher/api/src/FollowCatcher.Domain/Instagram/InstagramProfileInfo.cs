namespace FollowCatcher.Domain.Instagram;

public record InstagramProfileInfo(
    long Id,
    string Username,
    string FullName,
    long FollowerCount,
    long FollowingCount,
    string ProfilePictureUrl,
    int PostCount
);
