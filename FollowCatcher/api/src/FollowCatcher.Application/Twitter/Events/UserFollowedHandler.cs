using FollowCatcher.Application.Twitter.Commands.SendTweet;
using FollowCatcher.Domain.Instagram;
using Microsoft.Extensions.Logging;
using Space.Abstraction;
using Space.Abstraction.Attributes;
using Space.Abstraction.Context;

namespace FollowCatcher.Application.Twitter.Events;

public class UserFollowedHandler(ISpace space, ILogger<UserFollowedHandler> logger)
{
    [Notification]
    public async ValueTask Handle(NotificationContext<UserFollowedEvent> ctx)
    {
        var @event = ctx.Request;
        var message = $"{@event.MonitoredUsername} followed {@event.FollowedUsername}!";
        var tweetId = await space.Send(new SendTweetCommand(message, @event.MonitoredProfileCardInfo), ct: ctx.CancellationToken);
        logger.LogInformation("Sent tweet with id {TweetId} for followed user {FollowedUsername} by monitored user {MonitoredUsername}", tweetId, @event.FollowedUsername, @event.MonitoredUsername);
    }
}
