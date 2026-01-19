using FollowCatcher.Domain.Common;

namespace FollowCatcher.Domain.Instagram;

public record UserFollowedEvent(string MonitoredUsername, string FollowedUsername, byte[] MonitoredProfileCardInfo) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}

public record UserUnfollowedEvent(string MonitoredUsername, string UnfollowedUsername, byte[] MonitoredProfileCardInfo) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
