using FollowCatcher.Domain.Twitter;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text;
using Tweetinvi;

namespace FollowCatcher.Infrastructure.Twitter;

public sealed class TwitterService(IOptions<TwitterSettings> options, ILogger<TwitterService> logger) : ITwitterService
{
    private const string TweetsEndpoint = "https://api.twitter.com/2/tweets";

    private readonly TwitterClient _client = new TwitterClient(
            options.Value.ApiKey,
            options.Value.ApiKeySecret,
            options.Value.AccessToken,
            options.Value.AccessTokenSecret
        );

    public async Task<long> UploadImageAsync(byte[] imageBytes, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(imageBytes);

        if (imageBytes.Length == 0)
        {
            throw new ArgumentException("Image bytes cannot be empty.", nameof(imageBytes));
        }

        logger.LogDebug("Uploading image to Twitter. Size: {Size} bytes", imageBytes.Length);

        try
        {
            var media = await _client.Upload.UploadTweetImageAsync(imageBytes);

            if (media?.Id is null)
            {
                logger.LogError("Twitter image upload returned null or empty media ID");
                throw new TwitterException(
                    TwitterErrorCode.ImageUploadFailed,
                    "Failed to upload image to Twitter. Response was null or missing media ID.");
            }

            logger.LogInformation("Image uploaded successfully. MediaId: {MediaId}", media.Id);
            return media.Id.Value;
        }
        catch (TwitterException)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while uploading image to Twitter");
            throw new TwitterException(
                TwitterErrorCode.ImageUploadFailed,
                "An unexpected error occurred while uploading image to Twitter.",
                ex);
        }
    }

    public async Task<string> PublishTweetAsync(
        string text,
        long? mediaId = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(text);

        logger.LogDebug("Publishing tweet. HasMedia: {HasMedia}", mediaId.HasValue);

        try
        {
            var request = BuildTweetRequest(text, mediaId);
            var jsonBody = _client.Json.Serialize(request);

            var result = await _client.Execute.AdvanceRequestAsync(httpRequest =>
            {
                httpRequest.Query.Url = TweetsEndpoint;
                httpRequest.Query.HttpMethod = Tweetinvi.Models.HttpMethod.POST;
                httpRequest.Query.HttpContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            });

            return ProcessTweetResponse(result);
        }
        catch (TwitterException)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while publishing tweet");
            throw new TwitterException(
                TwitterErrorCode.TweetPublishFailed,
                "An unexpected error occurred while publishing tweet.",
                ex);
        }
    }

    private static TweetV2Request BuildTweetRequest(string text, long? mediaId)
    {
        return new TweetV2Request
        {
            Text = text,
            Media = mediaId.HasValue
                ? new TweetV2Media { MediaIds = [mediaId.Value.ToString()] }
                : null
        };
    }

    private string ProcessTweetResponse(Tweetinvi.Core.Web.ITwitterResult result)
    {
        if (!result.Response.IsSuccessStatusCode)
        {
            var statusCode = (HttpStatusCode)result.Response.StatusCode;
            var errorCode = MapHttpStatusToErrorCode(statusCode);

            logger.LogError(
                "Twitter API error. StatusCode: {StatusCode}, Content: {Content}",
                statusCode,
                result.Content);

            throw new TwitterException(
                errorCode,
                $"Twitter API returned {statusCode}: {result.Content}");
        }

        var response = _client.Json.Deserialize<TweetV2Response>(result.Content);

        if (string.IsNullOrEmpty(response?.Data?.Id))
        {
            logger.LogError("Twitter response missing tweet ID. Content: {Content}", result.Content);
            throw new TwitterException(
                TwitterErrorCode.InvalidResponse,
                "Twitter response was successful but missing tweet ID.");
        }

        logger.LogInformation("Tweet published successfully. TweetId: {TweetId}", response.Data.Id);
        return response.Data.Id;
    }

    private static TwitterErrorCode MapHttpStatusToErrorCode(HttpStatusCode statusCode)
    {
        return statusCode switch
        {
            HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden => TwitterErrorCode.Unauthorized,
            HttpStatusCode.TooManyRequests => TwitterErrorCode.RateLimitExceeded,
            _ => TwitterErrorCode.TweetPublishFailed
        };
    }
}
