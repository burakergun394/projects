using FollowCatcher.Domain.Instagram;
using MediatR;

namespace FollowCatcher.Application.Instagram.Queries.GetProfileCard;

public class GetProfileCardHandler(
    IInstagramService instagramService,
    IInstagramProfileCardGenerator cardGenerator)
    : IRequestHandler<GetProfileCardQuery, byte[]?>
{
    public async Task<byte[]?> Handle(GetProfileCardQuery request, CancellationToken cancellationToken)
    {
        // 1. Get Profile Info
        var profile = await instagramService.GetProfileInfoAsync(request.Username, cancellationToken);
        if (profile is null)
        {
            return null;
        }

        // 2. Download Avatar
        byte[] avatarBytes;
        using (var httpClient = new HttpClient())
        {
            // Note: In production, use IHttpClientFactory
             avatarBytes = await httpClient.GetByteArrayAsync(profile.ProfilePictureUrl, cancellationToken);
        }

        // 3. Generate Card
        return await cardGenerator.GenerateCardAsync(profile, avatarBytes);
    }
}
