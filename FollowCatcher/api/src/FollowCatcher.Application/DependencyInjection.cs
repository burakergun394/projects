using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FollowCatcher.Application;

/// <summary>
/// Dependency injection configuration for the Application layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers Application layer services with the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // Register MediatR for CQRS pattern
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
        });

        // Register FluentValidation validators
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
