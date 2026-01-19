using Newtonsoft.Json;

namespace FollowCatcher.Infrastructure.Twitter;

internal sealed class TweetV2Request
{
    [JsonProperty("text")]
    public required string Text { get; init; }

    [JsonProperty("media", NullValueHandling = NullValueHandling.Ignore)]
    public TweetV2Media? Media { get; init; }
}

internal sealed class TweetV2Media
{
    [JsonProperty("media_ids")]
    public required string[] MediaIds { get; init; }
}

internal sealed class TweetV2Response
{
    [JsonProperty("data")]
    public TweetV2Data? Data { get; init; }
}

internal sealed class TweetV2Data
{
    [JsonProperty("id")]
    public string? Id { get; init; }
}
