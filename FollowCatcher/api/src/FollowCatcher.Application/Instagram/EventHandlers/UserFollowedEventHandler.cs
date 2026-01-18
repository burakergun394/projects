using FollowCatcher.Application.Twitter.Commands.SendTweet;
using FollowCatcher.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FollowCatcher.Application.Instagram.EventHandlers;

public class UserFollowedEventHandler(ISender sender, ILogger<UserFollowedEventHandler> logger) : INotificationHandler<UserFollowedEvent>
{
    public async Task Handle(UserFollowedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling UserFollowedEvent: {Monitored} -> {Followed}", notification.MonitoredUsername, notification.FollowedUsername);

        var message = $"User {notification.MonitoredUsername} followed {notification.FollowedUsername}!";
        await sender.Send(new SendTweetCommand(message), cancellationToken);
    }
}
