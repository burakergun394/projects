using FollowCatcher.Application.Common.Configuration;
using FollowCatcher.Application.Common.Interfaces;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Logger;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FollowCatcher.Persistence.Services;

/// <summary>
/// Service for Instagram operations using InstagramApiSharp with best practices.
///
/// <para><b>Lifecycle: Singleton</b></para>
/// <para>
/// This service is registered as a singleton because:
/// - Session state is shared via file system (instagram_state.bin)
/// - Thread-safe initialization using SemaphoreSlim with double-check locking
/// - Better performance: avoids creating new instances per request
/// - Proper cleanup on application shutdown via IDisposable
/// </para>
///
/// <para><b>Thread Safety:</b></para>
/// <para>
/// All operations are thread-safe for concurrent requests:
/// - GetInstaApiAsync() uses SemaphoreSlim for initialization synchronization
/// - IInstaApi operations are thread-safe per InstagramApiSharp documentation
/// - Session state file I/O is synchronized through API instance
/// </para>
/// </summary>
public class InstagramService(
    ILogger<InstagramService> logger,
    IOptions<InstagramSettings> options) : IInstagramService, IDisposable
{
    private readonly InstagramSettings settings = options.Value;
    private readonly SemaphoreSlim initializationSemaphore = new(1, 1);
    private IInstaApi? instaApi;
    private bool disposed;

    private async Task<IInstaApi> GetInstaApiAsync()
    {
        if (instaApi is not null)
        {
            return instaApi;
        }

        await initializationSemaphore.WaitAsync();
        try
        {
            // Double-check locking pattern
            if (instaApi is not null)
            {
                return instaApi;
            }

            instaApi = await InitializeInstaApiAsync();
            return instaApi;
        }
        finally
        {
            initializationSemaphore.Release();
        }
    }

    /// <summary>
    /// Initializes the Instagram API client with best practices configuration.
    /// </summary>
    private async Task<IInstaApi> InitializeInstaApiAsync()
    {
        logger.LogInformation("Initializing Instagram API client");

        // Configure user session data
        var userSession = settings.UseAuthentication
            ? new UserSessionData
            {
                UserName = settings.Username!,
                Password = settings.Password!
            }
            : new UserSessionData(); // Empty for unauthenticated access

        // Configure request delay for rate limiting (recommended: 2 seconds)
        var delay = RequestDelay.FromSeconds(
            settings.RequestDelaySeconds,
            settings.RequestDelaySeconds);

        // Build API client with configuration
        var apiBuilder = InstaApiBuilder.CreateBuilder()
            .SetUser(userSession)
            .SetRequestDelay(delay);

        // Enable debug logging in development
        if (settings.EnableDebugLogging)
        {
            apiBuilder.UseLogger(new DebugLogger(InstagramApiSharp.Logger.LogLevel.Info));
            logger.LogInformation("Instagram debug logging enabled");
        }

        var api = apiBuilder.Build();

        // Try to load existing session state
        LoadSessionState(api);

        // Authenticate if credentials provided
        if (settings.UseAuthentication && !api.IsUserAuthenticated)
        {
            await AuthenticateAsync(api, delay);
        }

        return api;
    }

    /// <summary>
    /// Loads session state from persistent storage.
    /// </summary>
    private void LoadSessionState(IInstaApi api)
    {
        try
        {
            var stateFilePath = settings.StateFilePath;

            if (File.Exists(stateFilePath))
            {
                logger.LogInformation("Loading Instagram session state from {StateFile}", stateFilePath);

                using var fileStream = File.OpenRead(stateFilePath);
                api.LoadStateDataFromStream(fileStream);

                logger.LogInformation("Instagram session state loaded successfully");
            }
            else
            {
                logger.LogInformation("No existing session state found at {StateFile}", stateFilePath);
            }
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Failed to load Instagram session state");
        }
    }

    /// <summary>
    /// Saves session state to persistent storage.
    /// </summary>
    private void SaveSessionState(IInstaApi api)
    {
        try
        {
            var stateFilePath = settings.StateFilePath;

            logger.LogInformation("Saving Instagram session state to {StateFile}", stateFilePath);

            var state = api.GetStateDataAsStream();
            using var fileStream = File.Create(stateFilePath);
            state.Seek(0, SeekOrigin.Begin);
            state.CopyTo(fileStream);

            logger.LogInformation("Instagram session state saved successfully");
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Failed to save Instagram session state");
        }
    }

    /// <summary>
    /// Authenticates with Instagram using configured credentials.
    /// </summary>
    private async Task AuthenticateAsync(IInstaApi api, IRequestDelay delay)
    {
        try
        {
            logger.LogInformation("Authenticating with Instagram as {Username}", settings.Username);

            // Disable delay during authentication for faster login
            delay.Disable();

            var loginResult = await api.LoginAsync();

            // Re-enable delay after authentication
            delay.Enable();

            if (!loginResult.Succeeded)
            {
                logger.LogWarning("Instagram authentication failed: {Message}", loginResult.Info.Message);
                return;
            }

            logger.LogInformation("Instagram authentication successful");

            // Save authenticated session state
            SaveSessionState(api);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during Instagram authentication");
            delay.Enable(); // Ensure delay is re-enabled even on error
        }
    }

    /// <summary>
    /// Gets Instagram profile information for a given username.
    /// </summary>
    public async Task<InstagramProfileInfo?> GetProfileInfoAsync(
        string username,
        CancellationToken cancellationToken = default)
    {
        try
        {
            logger.LogInformation("Fetching Instagram profile for username: {Username}", username);

            var api = await GetInstaApiAsync();

            // Get full user information with stats
            var userInfoResult = await api.UserProcessor.GetUserInfoByUsernameAsync(username);

            if (!userInfoResult.Succeeded)
            {
                logger.LogWarning(
                    "Failed to fetch Instagram profile for {Username}. Error: {Error}",
                    username,
                    userInfoResult.Info.Message);
                return null;
            }

            var userInfo = userInfoResult.Value;

            var profileInfo = new InstagramProfileInfo(
                Username: userInfo.Username,
                FollowerCount: userInfo.FollowerCount,
                FollowingCount: userInfo.FollowingCount,
                ProfilePictureUrl: userInfo.ProfilePicUrl,
                PostCount: (int)userInfo.MediaCount
            );

            logger.LogInformation("Successfully fetched Instagram profile for {Username}", username);

            // Save session state after successful operation
            if (settings.UseAuthentication && api.IsUserAuthenticated)
            {
                SaveSessionState(api);
            }

            return profileInfo;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching Instagram profile for {Username}", username);
            return null;
        }
    }

    /// <summary>
    /// Disposes the Instagram API client and releases resources.
    /// </summary>
    public void Dispose()
    {
        if (disposed)
        {
            return;
        }

        if (instaApi is not null)
        {
            // Save final session state before disposal
            if (settings.UseAuthentication && instaApi.IsUserAuthenticated)
            {
                SaveSessionState(instaApi);
            }
        }

        initializationSemaphore?.Dispose();
        disposed = true;
        GC.SuppressFinalize(this);
    }
}
