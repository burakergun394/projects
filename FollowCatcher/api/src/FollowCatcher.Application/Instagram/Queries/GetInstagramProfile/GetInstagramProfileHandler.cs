using FollowCatcher.Domain.Data;
using FollowCatcher.Domain.Instagram;
using MediatR;

namespace FollowCatcher.Application.Instagram.Queries.GetInstagramProfile;


public class GetInstagramProfileHandler(IInstagramService instagramService)
    : IRequestHandler<GetInstagramProfileQuery, InstagramProfileDto?>
{
    public async Task<InstagramProfileDto?> Handle(GetInstagramProfileQuery request, CancellationToken cancellationToken)
    {
        var profileInfo = await instagramService.GetProfileInfoAsync(request.Username, cancellationToken);

        if (profileInfo is null)
        {
            return null;
        }

        return new InstagramProfileDto(
            profileInfo.Username,
            profileInfo.FollowerCount,
            profileInfo.FollowingCount,
            profileInfo.ProfilePictureUrl,
            profileInfo.PostCount
        );
    }
}
