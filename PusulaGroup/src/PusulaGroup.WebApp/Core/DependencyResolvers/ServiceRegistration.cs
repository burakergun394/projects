using PusulaGroup.WebApp.Core.CrossCuttingConcerns.Caching;
using PusulaGroup.WebApp.Core.Helpers;

namespace PusulaGroup.WebApp.Core.DependencyResolvers
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddCoreLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<StringHelper>();
            services.AddSingleton<ImageHelper>();
            services.AddSingleton<EmailHelper>();

            services.AddMemoryCache();
            services.AddSingleton<ICache, MicrosoftMemoryCache>();

            return services;
        }
    }
}
