using FollowCatcher.Application.Common.Configuration;
using FollowCatcher.Application.Common.Interfaces;
using FollowCatcher.Infrastructure.Services;
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

        return services;
    }
}
