using FluentValidation;
using FollowCatcher.Domain.Common;
using FollowCatcher.Domain.Twitter;
using MediatR;

namespace FollowCatcher.Application.Twitter.Commands.SendTweet;

public record SendTweetCommand(string Text) : IRequest<bool>;

public class SendTweetValidator : AbstractValidator<SendTweetCommand>
{
    public SendTweetValidator()
    {
        RuleFor(x => x.Text).NotEmpty().MaximumLength(280);
    }
}

public class SendTweetHandler(ITwitterService twitterService) : IRequestHandler<SendTweetCommand, bool>
{
    public async Task<bool> Handle(SendTweetCommand request, CancellationToken cancellationToken)
    {
        var tweetId = await twitterService.PublishTweetAsync(request.Text);
        return !string.IsNullOrEmpty(tweetId);
    }
}
