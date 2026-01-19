using FollowCatcher.Application.Instagram.Dtos;
using FollowCatcher.Domain.Instagram;
using MediatR;

namespace FollowCatcher.Application.Instagram.Queries.GetInstagramProfile;

public class GetInstagramProfileHandler(
    IInstagramService instagramService,
    IInstagramProfileCardGenerator cardGenerator,
    IHttpClientFactory httpClientFactory) : IRequestHandler<GetInstagramProfileQuery, InstagramProfileDto>
{
    public async Task<InstagramProfileDto> Handle(GetInstagramProfileQuery request, CancellationToken cancellationToken)
    {
        var profileInfo = await instagramService.GetProfileInfoAsync(request.Username, cancellationToken)
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
            avatarBytes = await httpClient.GetByteArrayAsync(profileInfo.ProfilePictureUrl, cancellationToken);
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
