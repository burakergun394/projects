using MediatR;

namespace FollowCatcher.Application.Twitter.Commands.ShareProfileCard;

public record ShareProfileCardCommand(string Username, string TweetText) : IRequest<string?>;
