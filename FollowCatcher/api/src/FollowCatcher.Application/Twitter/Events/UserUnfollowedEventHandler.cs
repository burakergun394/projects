using FollowCatcher.Application.Twitter.Commands.SendTweet;
using FollowCatcher.Domain.Instagram;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FollowCatcher.Application.Twitter.Events;

public class UserUnfollowedEventHandler(
    IServiceScopeFactory serviceScopeFactory,
    ILogger<UserUnfollowedEventHandler> logger) : INotificationHandler<UserUnfollowedEvent>
{
    public async Task Handle(UserUnfollowedEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            // Create own scope because caller uses fire-and-forget
            using var scope = serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var message = $"{notification.MonitoredUsername} unfollowed {notification.UnfollowedUsername}!";

            logger.LogInformation(
                "Handling UserUnfollowedEvent: {MonitoredUsername} -> {UnfollowedUsername}",
                notification.MonitoredUsername,
                notification.UnfollowedUsername);

            var tweetId = await mediator.Send(new SendTweetCommand(message, notification.MonitoredProfileCardInfo), cancellationToken);

            logger.LogInformation("Tweet sent successfully. TweetId: {TweetId}", tweetId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error handling UserUnfollowedEvent for {Username}", notification.MonitoredUsername);
        }
    }
}
