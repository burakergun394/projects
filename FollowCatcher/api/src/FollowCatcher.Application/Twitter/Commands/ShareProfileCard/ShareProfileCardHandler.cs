using FollowCatcher.Domain.Instagram;
using FollowCatcher.Domain.Twitter;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FollowCatcher.Application.Twitter.Commands.ShareProfileCard;

public class ShareProfileCardHandler(
    IInstagramService instagramService,
    IInstagramProfileCardGenerator cardGenerator,
    ITwitterService twitterService,
    ILogger<ShareProfileCardHandler> logger) : IRequestHandler<ShareProfileCardCommand, string?>
{
    private readonly HttpClient httpClient = new();

    public async Task<string?> Handle(ShareProfileCardCommand request, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Processing ShareProfileCardCommand for {Username}", request.Username);

            // 1. Fetch Instagram Profile
            var profile = await instagramService.GetProfileInfoAsync(request.Username, cancellationToken);
            if (profile == null)
            {
                logger.LogWarning("Instagram profile not found: {Username}", request.Username);
                return null;
            }

            // 2. Download Avatar
            byte[] avatarBytes;
            try 
            {
                 avatarBytes = await httpClient.GetByteArrayAsync(profile.ProfilePictureUrl, cancellationToken);
            }
            catch (Exception ex)
            {
                 logger.LogError(ex, "Failed to download avatar from {Url}", profile.ProfilePictureUrl);
                 return null;
            }

            // 3. Generate Card
            var cardBytes = await cardGenerator.GenerateCardAsync(profile, avatarBytes);

            // 4. Upload to Twitter
            var mediaId = await twitterService.UploadImageAsync(cardBytes);

            // 5. Publish Tweet
            var tweetId = await twitterService.PublishTweetAsync(request.TweetText, mediaId);

            return tweetId;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error handling ShareProfileCardCommand");
            throw;
        }
    }
}
