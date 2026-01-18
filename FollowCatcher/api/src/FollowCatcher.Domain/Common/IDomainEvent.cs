namespace FollowCatcher.Domain.Common;


using MediatR;

public interface IDomainEvent : INotification
{

    DateTime OccurredOn { get; }
}
