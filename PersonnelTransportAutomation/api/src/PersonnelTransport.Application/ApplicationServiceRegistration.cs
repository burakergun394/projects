using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace PersonnelTransport.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Space library likely scans for handlers, but if we have other application services (validators, etc)
        // we would register them here.
        // For now, we return services to keep the pattern consistent.

        return services;
    }
}
