namespace FollowCatcher.Application.Instagram.Queries;


public record InstagramProfileDto(
    string Username,
    long FollowerCount,
    long FollowingCount,
    string ProfilePictureUrl,
    int PostCount,
    byte[] ProfileCardImage
);
