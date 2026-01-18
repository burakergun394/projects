using FollowCatcher.Domain.Instagram;
using FollowCatcher.Domain.Data;
using FollowCatcher.Infrastructure.Instagram;
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
        services.AddSingleton<IInstagramService, InstagramService>();
        services.AddScoped<IInstagramProfileCardGenerator, SkiaInstagramProfileCardGenerator>();

        return services;
    }
}
