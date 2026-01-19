using FollowCatcher.Application.Twitter.Commands.SendTweet;
using FollowCatcher.Domain.Instagram;
using Microsoft.Extensions.Logging;
using Space.Abstraction;
using Space.Abstraction.Attributes;
using Space.Abstraction.Context;

namespace FollowCatcher.Application.Twitter.Events;

public class UserUnfollowedHandler(ISpace space, ILogger<UserUnfollowedHandler> logger)
{
    [Notification]
    public async ValueTask Handle(NotificationContext<UserUnfollowedEvent> ctx)
    {
        var @event = ctx.Request;
        var message = $"{@event.MonitoredUsername} unfollowed {@event.UnfollowedUsername}.";
        var tweetId = await space.Send(new SendTweetCommand(message, @event.MonitoredProfileCardInfo), ct: ctx.CancellationToken);
        logger.LogInformation("Sent tweet with id {TweetId} for unfollowed user {UnfollowedUsername} by monitored user {MonitoredUsername}", tweetId, @event.UnfollowedUsername, @event.MonitoredUsername);
    }
}
