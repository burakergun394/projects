using FollowCatcher.Domain.Instagram;
using FollowCatcher.Domain.Data;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Logger;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FollowCatcher.Infrastructure.Instagram;


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

    private async Task<IInstaApi> InitializeInstaApiAsync()
    {
        logger.LogInformation("Initializing Instagram API client");


        var userSession = settings.UseAuthentication
            ? new UserSessionData
            {
                UserName = settings.Username!,
                Password = settings.Password!
            }
            : new UserSessionData();

        var delay = RequestDelay.FromSeconds(
            settings.RequestDelaySeconds,
            settings.RequestDelaySeconds);


        var apiBuilder = InstaApiBuilder.CreateBuilder()
            .SetUser(userSession)
            .SetRequestDelay(delay);

        if (settings.EnableDebugLogging)
        {
            apiBuilder.UseLogger(new DebugLogger(InstagramApiSharp.Logger.LogLevel.Info));
            logger.LogInformation("Instagram debug logging enabled");
        }

        var api = apiBuilder.Build();

        LoadSessionState(api);


        if (settings.UseAuthentication && !api.IsUserAuthenticated)
        {
            await AuthenticateAsync(api, delay);
        }

        return api;
    }

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

    private async Task AuthenticateAsync(IInstaApi api, IRequestDelay delay)
    {
        try
        {
            logger.LogInformation("Authenticating with Instagram as {Username}", settings.Username);

            delay.Disable();

            var loginResult = await api.LoginAsync();
            delay.Enable();

            if (!loginResult.Succeeded)
            {
                logger.LogWarning("Instagram authentication failed: {Message}", loginResult.Info.Message);
                return;
            }

            logger.LogInformation("Instagram authentication successful");


            SaveSessionState(api);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during Instagram authentication");
            delay.Enable();
        }
    }

    public async Task<InstagramProfileInfo?> GetProfileInfoAsync(
        string username,
        CancellationToken cancellationToken = default)
    {
        try
        {
            logger.LogInformation("Fetching Instagram profile for username: {Username}", username);

            var api = await GetInstaApiAsync();

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
                FullName: userInfo.FullName,
                FollowerCount: userInfo.FollowerCount,
                FollowingCount: userInfo.FollowingCount,
                ProfilePictureUrl: userInfo.ProfilePicUrl,
                PostCount: (int)userInfo.MediaCount
            );

            logger.LogInformation("Successfully fetched Instagram profile for {Username}", username);

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

    public void Dispose()
    {
        if (disposed)
        {
            return;
        }

        if (instaApi is not null)
        {
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
