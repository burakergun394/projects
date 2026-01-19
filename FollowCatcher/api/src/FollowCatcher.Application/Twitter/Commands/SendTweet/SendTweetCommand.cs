using FollowCatcher.Domain.Twitter;
using Space.Abstraction.Attributes;
using Space.Abstraction.Context;
using Space.Abstraction.Contracts;

namespace FollowCatcher.Application.Twitter.Commands.SendTweet;

public record SendTweetCommand(string Text, byte[]? Image = null) : IRequest<string>;

public class SendTweetHandler(ITwitterService twitterService)
{
    [Handle]
    public async ValueTask<string> Handle(HandlerContext<SendTweetCommand> ctx)
    {
        var request = ctx.Request;
        long? mediaId = null;
        if (request.Image is { Length: > 0 })
            mediaId = await twitterService.UploadImageAsync(request.Image);

        var tweetId = await twitterService.PublishTweetAsync(request.Text, mediaId);
        return tweetId ?? string.Empty;
    }
}
