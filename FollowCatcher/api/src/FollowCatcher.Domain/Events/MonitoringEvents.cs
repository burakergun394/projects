using FollowCatcher.Domain.Common;

namespace FollowCatcher.Domain.Events;

public record UserFollowedEvent(string MonitoredUsername, string FollowedUsername, string FollowedUserId) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}

public record UserUnfollowedEvent(string MonitoredUsername, string UnfollowedUsername, string UnfollowedUserId) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
