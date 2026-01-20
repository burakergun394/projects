using MediatR;

namespace FollowCatcher.Application.Instagram.Commands.CreateInstagramTrackedAccount;

public record CreateInstagramTrackedAccountCommand(string Username) : IRequest<Guid>;
