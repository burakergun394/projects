using FluentValidation;
using FollowCatcher.Application.Instagram.BackgroundServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FollowCatcher.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();
        var assembly = Assembly.GetExecutingAssembly();

        // Register MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        // Register FluentValidation validators
        services.AddValidatorsFromAssembly(assembly);

        // Configuration
        services.Configure<Instagram.InstagramMonitoringSettings>(
            configuration.GetSection(Instagram.InstagramMonitoringSettings.SectionName));

        // Register Background Services
        services.AddHostedService<InstagramTrackedAccountBackgroundService>();

        return services;
    }
}
