using FollowCatcher.Application.Instagram.Dtos;
using MediatR;

namespace FollowCatcher.Application.Instagram.Queries.GetInstagramProfile;

public record GetInstagramProfileQuery(string Username, bool IncludeProfileCard) : IRequest<InstagramProfileDto>;
