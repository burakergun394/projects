using FollowCatcher.Application.Twitter.Commands.SendTweet;
using FollowCatcher.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FollowCatcher.Application.Instagram.EventHandlers;

public class UserUnfollowedEventHandler(ISender sender, ILogger<UserUnfollowedEventHandler> logger) : INotificationHandler<UserUnfollowedEvent>
{
    public async Task Handle(UserUnfollowedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling UserUnfollowedEvent: {Monitored} -> {Unfollowed}", notification.MonitoredUsername, notification.UnfollowedUsername);

        var message = $"User {notification.MonitoredUsername} unfollowed {notification.UnfollowedUsername}!";
        await sender.Send(new SendTweetCommand(message), cancellationToken);
    }
}
