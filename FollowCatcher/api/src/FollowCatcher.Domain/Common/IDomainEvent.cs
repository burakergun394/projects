using MediatR;

namespace FollowCatcher.Domain.Common;

public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; }
}
