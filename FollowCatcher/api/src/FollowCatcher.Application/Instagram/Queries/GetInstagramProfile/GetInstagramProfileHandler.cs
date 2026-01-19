using FollowCatcher.Application.Instagram.Dtos;
using FollowCatcher.Domain.Instagram;
using Space.Abstraction.Attributes;
using Space.Abstraction.Context;

namespace FollowCatcher.Application.Instagram.Queries.GetInstagramProfile;


public class GetInstagramProfileHandler(IInstagramService instagramService,
                                        IInstagramProfileCardGenerator cardGenerator,
                                        IHttpClientFactory httpClientFactory)
{
    [Handle]
    public async ValueTask<InstagramProfileDto> Handle(HandlerContext<GetInstagramProfileQuery> ctx)
    {
        var request = ctx.Request;
        var profileInfo = await instagramService.GetProfileInfoAsync(request.Username, ctx.CancellationToken)
                          ?? throw new InvalidOperationException($"Profile not found for username: {request.Username}");

        if (request.IncludeProfileCard is false)
        {
            return new InstagramProfileDto(
                profileInfo.Username,
                profileInfo.FollowerCount,
                profileInfo.FollowingCount,
                profileInfo.ProfilePictureUrl,
                profileInfo.PostCount,
                []
            );
        }

        byte[] avatarBytes;
        using (var httpClient = httpClientFactory.CreateClient())
        {
            avatarBytes = await httpClient.GetByteArrayAsync(profileInfo.ProfilePictureUrl, ctx.CancellationToken);
        }

        var profileCardImage = await cardGenerator.GenerateCardAsync(profileInfo, avatarBytes);
        return new InstagramProfileDto(
            profileInfo.Username,
            profileInfo.FollowerCount,
            profileInfo.FollowingCount,
            profileInfo.ProfilePictureUrl,
            profileInfo.PostCount,
            profileCardImage
        );
    }
}
