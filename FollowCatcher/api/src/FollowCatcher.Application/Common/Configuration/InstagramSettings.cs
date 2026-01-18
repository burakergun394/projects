namespace FollowCatcher.Application.Common.Configuration;


public class InstagramSettings
{
    public const string SectionName = "Instagram";


    public string? Username { get; set; }


    public string? Password { get; set; }


    public string StateFilePath { get; set; } = "instagram_state.bin";


    public bool EnableDebugLogging { get; set; }


    public int RequestDelaySeconds { get; set; } = 2;


    public bool UseAuthentication => !string.IsNullOrWhiteSpace(Username) &&
                                     !string.IsNullOrWhiteSpace(Password);
}
