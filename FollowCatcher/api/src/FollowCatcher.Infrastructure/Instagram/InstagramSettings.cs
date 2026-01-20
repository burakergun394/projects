namespace FollowCatcher.Infrastructure.Instagram;

public class InstagramSettings
{
    public const string SectionName = "Instagram";


    public string? Username { get; set; }


    public string? Password { get; set; }


    public string StateFilePath { get; set; } = "instagram_state.bin";


    public bool EnableDebugLogging { get; set; }


    public int RequestDelayMinSeconds { get; set; } = 2;
    public int RequestDelayMaxSeconds { get; set; } = 5;


    public bool UseAuthentication => !string.IsNullOrWhiteSpace(Username) &&
                                     !string.IsNullOrWhiteSpace(Password);
}
