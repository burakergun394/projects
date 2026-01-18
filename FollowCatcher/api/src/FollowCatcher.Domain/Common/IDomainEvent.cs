namespace FollowCatcher.Domain.Common;

/// <summary>
/// Marker interface for domain events.
/// Domain events represent something significant that happened in the domain.
/// </summary>
public interface IDomainEvent
{
    /// <summary>
    /// Timestamp when the event occurred.
    /// </summary>
    DateTime OccurredOn { get; }
}
