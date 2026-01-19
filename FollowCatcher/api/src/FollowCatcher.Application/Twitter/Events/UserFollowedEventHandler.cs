using FollowCatcher.Application.Twitter.Commands.SendTweet;
using FollowCatcher.Domain.Instagram;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FollowCatcher.Application.Twitter.Events;

public class UserFollowedEventHandler(
    IServiceScopeFactory serviceScopeFactory,
    ILogger<UserFollowedEventHandler> logger) : INotificationHandler<UserFollowedEvent>
{
    public async Task Handle(UserFollowedEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            // Create own scope because caller uses fire-and-forget
            using var scope = serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var message = $"{notification.MonitoredUsername} followed {notification.FollowedUsername}!";

            logger.LogInformation(
                "Handling UserFollowedEvent: {MonitoredUsername} -> {FollowedUsername}",
                notification.MonitoredUsername,
                notification.FollowedUsername);

            var tweetId = await mediator.Send(new SendTweetCommand(message, notification.MonitoredProfileCardInfo), cancellationToken);

            logger.LogInformation("Tweet sent successfully. TweetId: {TweetId}", tweetId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error handling UserFollowedEvent for {Username}", notification.MonitoredUsername);
        }
    }
}
