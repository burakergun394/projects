using MediatR;

namespace FollowCatcher.Application.Instagram.Commands.DeleteInstagramTrackedAccount;

public record DeleteInstagramTrackedAccountCommand(Guid Id) : IRequest;
