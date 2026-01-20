using FollowCatcher.Application.Instagram.Dtos;
using MediatR;

namespace FollowCatcher.Application.Instagram.Queries.GetInstagramTrackedAccountById;

public record GetInstagramTrackedAccountByIdQuery(Guid Id) : IRequest<InstagramTrackedAccountDto?>;
