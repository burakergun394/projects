namespace FollowCatcher.Domain.Common;


public interface IDomainEvent
{

    DateTime OccurredOn { get; }
}
