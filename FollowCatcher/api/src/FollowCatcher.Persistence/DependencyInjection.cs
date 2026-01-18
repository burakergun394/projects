using FollowCatcher.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FollowCatcher.Persistence;

/// <summary>
/// Dependency injection configuration for the Persistence layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers Persistence layer services with the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <param name="configuration">The configuration to use for service registration.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        // Register DbContext with SQL Server
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
            });

            // Enable sensitive data logging in development
            // options.EnableSensitiveDataLogging();
            // options.EnableDetailedErrors();
        });

        // Register IApplicationDbContext
        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        // Register IUnitOfWork
        services.AddScoped<IUnitOfWork>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        // Register repositories here
        // Example: services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        return services;
    }
}
