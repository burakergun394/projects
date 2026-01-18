using MediatR;

namespace FollowCatcher.Application.Instagram.Queries.GetInstagramProfile;

/// <summary>
/// Query to get Instagram profile information.
/// </summary>
public record GetInstagramProfileQuery(string Username) : IRequest<InstagramProfileDto?>;
