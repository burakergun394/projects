using Newtonsoft.Json;

namespace FollowCatcher.Infrastructure.Twitter;

internal class TweetV2Request
{
    [JsonProperty("text")]
    public string Text { get; set; } = string.Empty;

    [JsonProperty("media")]
    public TweetV2Media? Media { get; set; }
}

internal class TweetV2Media
{
    [JsonProperty("media_ids")]
    public string[] MediaIds { get; set; } = Array.Empty<string>();
}

internal class TweetV2Response
{
    [JsonProperty("data")]
    public TweetV2Data? Data { get; set; }
}

internal class TweetV2Data
{
    [JsonProperty("id")]
    public string? Id { get; set; }
}
