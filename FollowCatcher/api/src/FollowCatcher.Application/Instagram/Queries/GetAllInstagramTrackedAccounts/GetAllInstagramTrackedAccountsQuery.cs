using FollowCatcher.Application.Instagram.Dtos;
using MediatR;

namespace FollowCatcher.Application.Instagram.Queries.GetAllInstagramTrackedAccounts;

public record GetAllInstagramTrackedAccountsQuery : IRequest<IEnumerable<InstagramTrackedAccountDto>>;
