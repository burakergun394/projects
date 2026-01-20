using MediatR;

namespace FollowCatcher.Application.Instagram.Commands.CreateInstagramTrackedAccountsBatch;

public record CreateInstagramTrackedAccountsBatchCommand(List<string> Usernames) : IRequest<List<Guid>>;
