namespace FollowCatcher.Infrastructure.Twitter;

public class TwitterSettings
{
    public const string SectionName = "Twitter";

    public string ApiKey { get; set; } = string.Empty;
    public string ApiKeySecret { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string AccessTokenSecret { get; set; } = string.Empty;
}
