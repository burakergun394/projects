using FollowCatcher.Domain.Instagram;
using FollowCatcher.Domain.Twitter;
using FollowCatcher.Infrastructure.Instagram;
using FollowCatcher.Infrastructure.Twitter;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FollowCatcher.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure Instagram settings
        var instagramSection = configuration.GetSection(InstagramSettings.SectionName);
        services.Configure<InstagramSettings>(instagramSection);

        // Register Instagram service
        services.AddSingleton<IInstagramService, InstagramApiSharpService>();
        services.AddScoped<IInstagramProfileCardGenerator, InstagramSkiaProfileCardGenerator>();

        // Configure Twitter settings
        var twitterSection = configuration.GetSection(TwitterSettings.SectionName);
        services.Configure<TwitterSettings>(twitterSection);

        // Register Twitter service
        services.AddSingleton<ITwitterService, TwitterService>();

        return services;
    }
}
