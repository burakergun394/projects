using FollowCatcher.Domain.Twitter;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using Tweetinvi;

namespace FollowCatcher.Infrastructure.Twitter;

public class TwitterService(
    IOptions<TwitterSettings> options,
    ILogger<TwitterService> logger) : ITwitterService
{
    private readonly TwitterClient client = new(
        options.Value.ApiKey,
        options.Value.ApiKeySecret,
        options.Value.AccessToken,
        options.Value.AccessTokenSecret
    );

    public async Task<long> UploadImageAsync(byte[] imageBytes)
    {
        try
        {
            logger.LogInformation("Uploading image to Twitter...");
            
            var media = await client.Upload.UploadTweetImageAsync(imageBytes);
            
            if (media == null)
            {
                logger.LogError("Twitter image upload returned null.");
                throw new Exception("Failed to upload image to Twitter.");
            }

            logger.LogInformation("Image uploaded successfully. Media ID: {MediaId}", media.Id);
            return media.Id.GetValueOrDefault();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error uploading image to Twitter.");
            throw;
        }
    }

    public async Task<string?> PublishTweetAsync(string text, long? mediaId = null)
    {
        try
        {
            logger.LogInformation("Publishing tweet (V2): {Text} with Media ID: {MediaId}", text, mediaId?.ToString() ?? "None");

            var tweetRequest = new TweetV2Request
            {
                Text = text,
                Media = mediaId.HasValue 
                    ? new TweetV2Media { MediaIds = [mediaId.Value.ToString()] }
                    : null
            };

            var jsonBody = client.Json.Serialize(tweetRequest);

            var result = await client.Execute.AdvanceRequestAsync(request =>
            {
                request.Query.Url = "https://api.twitter.com/2/tweets";
                request.Query.HttpMethod = Tweetinvi.Models.HttpMethod.POST;
                request.Query.HttpContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            });

            if (!result.Response.IsSuccessStatusCode)
            {
                logger.LogError("Twitter publish failed. Code: {Code}, Content: {Content}", result.Response.StatusCode, result.Content);
                return null;
            }

            var response = client.Json.Deserialize<TweetV2Response>(result.Content);
            
            if (!string.IsNullOrEmpty(response?.Data?.Id))
            {
                logger.LogInformation("Tweet published successfully. ID: {TweetId}", response.Data.Id);
                return response.Data.Id;
            }

            logger.LogError("Twitter response missing ID. Content: {Content}", result.Content);
            return null;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error publishing tweet.");
            throw;
        }
    }
}