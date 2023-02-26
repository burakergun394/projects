using Microsoft.Extensions.DependencyInjection;

namespace PusulaGroup.Application.DependencyResolvers;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
    {
        //services.AddScoped<ITourApplicationService, TourApplicationService>();
        //services.AddScoped<ITourImageApplicationService, TourImageApplicationService>();
        //services.AddScoped<IServiceApplicationService, ServiceApplicationService>();
        //services.AddScoped<IServiceImageApplicationService, ServiceImageApplicationService>();
        //services.AddSingleton<ImageHelper>();

        return services;
    }
}
