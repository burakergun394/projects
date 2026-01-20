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
            options.UseNpgsql(connectionString);
        });

        // Register IApplicationDbContext
        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        // Register IUnitOfWork
        services.AddScoped<IUnitOfWork>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        // Register repositories here
        services.AddScoped<IInstagramTrackedAccountRepository, InstagramTrackedAccountRepository>();

        return services;
    }
}
