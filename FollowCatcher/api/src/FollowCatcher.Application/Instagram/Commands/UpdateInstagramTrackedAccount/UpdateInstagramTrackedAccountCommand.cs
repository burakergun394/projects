using MediatR;

namespace FollowCatcher.Application.Instagram.Commands.UpdateInstagramTrackedAccount;

public record UpdateInstagramTrackedAccountCommand(Guid Id, string Username) : IRequest;
