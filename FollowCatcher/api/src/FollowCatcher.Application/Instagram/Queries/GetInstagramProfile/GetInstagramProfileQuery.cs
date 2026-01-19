using FollowCatcher.Application.Instagram.Dtos;
using Space.Abstraction.Contracts;

namespace FollowCatcher.Application.Instagram.Queries.GetInstagramProfile;


public record GetInstagramProfileQuery(string Username, bool IncludeProfileCard) : IRequest<InstagramProfileDto>;
