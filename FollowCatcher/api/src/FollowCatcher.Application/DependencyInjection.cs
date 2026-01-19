using FluentValidation;
using FollowCatcher.Application.Instagram.BackgroundServices;
using Microsoft.Extensions.DependencyInjection;
using Space.Abstraction;
using Space.DependencyInjection;
using System.Reflection;

namespace FollowCatcher.Application;


public static class DependencyInjection
{

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddHttpClient();
        var assembly = Assembly.GetExecutingAssembly();

        // Register Space for CQRS pattern
        services.AddSpace(opt =>
        {
            opt.NotificationDispatchType = NotificationDispatchType.Parallel;
        });

        // Register FluentValidation validators
        services.AddValidatorsFromAssembly(assembly);

        // Register Background Services
        services.AddHostedService<InstagramTrackedAccountBackgroundService>();

        return services;
    }
}
