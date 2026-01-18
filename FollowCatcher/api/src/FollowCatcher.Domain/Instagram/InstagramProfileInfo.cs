namespace FollowCatcher.Domain.Instagram;

public record InstagramProfileInfo(
    string Username,
    long FollowerCount,
    long FollowingCount,
    string ProfilePictureUrl,
    int PostCount
);
