using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonnelTransport.Application.Contracts;
using PersonnelTransport.Infrastructure.GoogleMaps;

namespace PersonnelTransport.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Google Maps
        services.Configure<GoogleMapsSettings>(
            configuration.GetSection(GoogleMapsSettings.SectionName));

        services.AddHttpClient<IRoutingService, GoogleMapsRoutingService>();

        return services;
    }
}
