using FollowCatcher.Domain.Twitter;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Net.Http;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Core.Web;

namespace FollowCatcher.Infrastructure.Twitter;

public class TwitterService(
    IOptions<TwitterSettings> options,
    ILogger<TwitterService> logger) : ITwitterService
{
    private readonly TwitterSettings settings = options.Value;
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

    public async Task<string?> PublishTweetAsync(string text, long mediaId)
    {
        try
        {
            logger.LogInformation("Publishing tweet (V2 Workaround): {Text} with Media ID: {MediaId}", text, mediaId);

            var result = await client.Execute.AdvanceRequestAsync((ITwitterRequest request) =>
            {
                var body = new
                {
                    text = text,
                    media = new { media_ids = new[] { mediaId.ToString() } }
                };

                var jsonBody = System.Text.Json.JsonSerializer.Serialize(body);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                request.Query.Url = "https://api.twitter.com/2/tweets";
                request.Query.HttpMethod = Tweetinvi.Models.HttpMethod.POST;
                request.Query.HttpContent = content;
            });

            if (result.Response.IsSuccessStatusCode == false)
            {
                logger.LogError("Twitter publish (V2 Workaround) failed. Code: {Code}, Content: {Content}", result.Response.StatusCode, result.Content);
                return null;
            }

            var json = System.Text.Json.JsonDocument.Parse(result.Content);
            if (json.RootElement.TryGetProperty("data", out var data) && 
                data.TryGetProperty("id", out var idElement))
            {
                var id = idElement.GetString();
                logger.LogInformation("Tweet published successfully (V2). Tweet ID: {TweetId}", id);
                return id;
            }

            logger.LogError("Twitter response (V2) did not contain tweet ID. Content: {Content}", result.Content);
            return null;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error publishing tweet (V2 Workaround).");
            throw;
        }
    }
}
