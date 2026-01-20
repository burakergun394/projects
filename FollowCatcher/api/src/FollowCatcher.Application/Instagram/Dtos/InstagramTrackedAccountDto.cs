namespace FollowCatcher.Application.Instagram.Dtos;

public record InstagramTrackedAccountDto(
    Guid Id,
    string Username,
    int FollowingCount,
    DateTime LastChecked,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
