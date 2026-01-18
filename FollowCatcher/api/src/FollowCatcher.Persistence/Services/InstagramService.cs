using FollowCatcher.Application.Common.Interfaces;
using InstagramApiSharp;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using Microsoft.Extensions.Logging;

namespace FollowCatcher.Persistence.Services;

/// <summary>
/// Service for Instagram operations using InstagramApiSharp.
/// </summary>
public class InstagramService : IInstagramService
{
    private readonly ILogger<InstagramService> logger;
    private readonly IInstaApi instaApi;

    public InstagramService(ILogger<InstagramService> logger)
    {
        this.logger = logger;

        // Create Instagram API client without authentication (for public profile access)
        var userSessionData = new UserSessionData();
        this.instaApi = InstaApiBuilder.CreateBuilder()
            .SetUser(userSessionData)
            .Build();
    }

    /// <summary>
    /// Gets Instagram profile information for a given username.
    /// </summary>
    public async Task<InstagramProfileInfo?> GetProfileInfoAsync(string username, CancellationToken cancellationToken = default)
    {
        try
        {
            logger.LogInformation("Fetching Instagram profile for username: {Username}", username);

            // Get full user information with stats
            var userInfoResult = await this.instaApi.UserProcessor.GetUserInfoByUsernameAsync(username);

            if (!userInfoResult.Succeeded)
            {
                logger.LogWarning("Failed to fetch Instagram profile for {Username}. Error: {Error}",
                    username, userInfoResult.Info.Message);
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

            return profileInfo;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching Instagram profile for {Username}", username);
            return null;
        }
    }
}
