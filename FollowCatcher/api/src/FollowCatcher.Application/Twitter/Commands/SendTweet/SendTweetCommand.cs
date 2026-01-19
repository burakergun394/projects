using FollowCatcher.Domain.Twitter;
using MediatR;

namespace FollowCatcher.Application.Twitter.Commands.SendTweet;

public record SendTweetCommand(string Text, byte[]? Image = null) : IRequest<string>;

public class SendTweetHandler(ITwitterService twitterService) : IRequestHandler<SendTweetCommand, string>
{
    public async Task<string> Handle(SendTweetCommand request, CancellationToken cancellationToken)
    {
        long? mediaId = null;
        if (request.Image is { Length: > 0 })
            mediaId = await twitterService.UploadImageAsync(request.Image, cancellationToken);

        return await twitterService.PublishTweetAsync(request.Text, mediaId, cancellationToken);
    }
}
