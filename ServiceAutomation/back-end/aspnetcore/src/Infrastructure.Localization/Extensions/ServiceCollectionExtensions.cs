using Application.Abstractions.Localizations;
using Infrastructure.Localization.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Localization.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLocalizationServices(this IServiceCollection services, IConfiguration configuration)
    {
    
        services.AddSingleton<ILocalizationService, LocalizationService>();

        return services;
    }
}