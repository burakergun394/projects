namespace FollowCatcher.Application.Common.Configuration;

/// <summary>
/// Configuration settings for Instagram API integration.
/// </summary>
public class InstagramSettings
{
    public const string SectionName = "Instagram";

    /// <summary>
    /// Instagram username for authenticated requests (optional).
    /// Leave empty for unauthenticated/public access.
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Instagram password for authenticated requests (optional).
    /// Leave empty for unauthenticated/public access.
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// Path to session state file for persistence.
    /// Default: "instagram_state.bin"
    /// </summary>
    public string StateFilePath { get; set; } = "instagram_state.bin";

    /// <summary>
    /// Enable debug logging for Instagram API calls.
    /// Default: false (enable in Development)
    /// </summary>
    public bool EnableDebugLogging { get; set; }

    /// <summary>
    /// Request delay in seconds to respect rate limiting.
    /// Default: 2 seconds (recommended by InstagramApiSharp)
    /// </summary>
    public int RequestDelaySeconds { get; set; } = 2;

    /// <summary>
    /// Whether to use authenticated mode.
    /// Requires Username and Password to be set.
    /// </summary>
    public bool UseAuthentication => !string.IsNullOrWhiteSpace(Username) &&
                                     !string.IsNullOrWhiteSpace(Password);
}
