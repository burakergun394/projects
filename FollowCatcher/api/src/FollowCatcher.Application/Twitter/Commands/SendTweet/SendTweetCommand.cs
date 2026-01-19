using FollowCatcher.Domain.Common;
using FollowCatcher.Domain.Twitter;
using Space.Abstraction.Contracts;
using Space.Abstraction.Attributes;
using Space.Abstraction.Context;

namespace FollowCatcher.Application.Twitter.Commands.SendTweet;

public record SendTweetCommand(string Text) : IRequest<bool>;

public class SendTweetHandler(ITwitterService twitterService)
{
    [Handle]
    public async ValueTask<bool> Handle(HandlerContext<SendTweetCommand> ctx)
    {
        var request = ctx.Request;
        var tweetId = await twitterService.PublishTweetAsync(request.Text);
        return !string.IsNullOrEmpty(tweetId);
    }
}
