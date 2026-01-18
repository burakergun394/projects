using FollowCatcher.Domain.Instagram;
using MediatR;

namespace FollowCatcher.Application.Instagram.Queries.GetProfileCard;

public record GetProfileCardQuery(string Username) : IRequest<byte[]?>;
