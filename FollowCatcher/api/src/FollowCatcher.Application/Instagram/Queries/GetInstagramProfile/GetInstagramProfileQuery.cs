using MediatR;

namespace FollowCatcher.Application.Instagram.Queries.GetInstagramProfile;


public record GetInstagramProfileQuery(string Username) : IRequest<InstagramProfileDto?>;
