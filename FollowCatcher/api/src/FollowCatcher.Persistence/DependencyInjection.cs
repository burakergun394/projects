using FollowCatcher.Domain.Data;
using FollowCatcher.Domain.Instagram;
using FollowCatcher.Persistence.Instagram;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FollowCatcher.Persistence;


public static class DependencyInjection
{

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
        services.AddScoped<IInstagramTrackedAccountRepository, InstagramTrackedAccountRepository>();
        // Register Database Seeder
        services.AddScoped<DatabaseSeeder>();


        return services;
    }
}
