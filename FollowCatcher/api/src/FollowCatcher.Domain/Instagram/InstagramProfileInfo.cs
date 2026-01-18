namespace FollowCatcher.Domain.Instagram;

public record InstagramProfileInfo(
    string Username,
    string FullName,
    long FollowerCount,
    long FollowingCount,
    string ProfilePictureUrl,
    int PostCount
);
