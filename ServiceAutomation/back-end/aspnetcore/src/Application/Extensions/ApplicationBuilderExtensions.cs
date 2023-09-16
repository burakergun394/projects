using Application.Contexts;
using Microsoft.AspNetCore.Builder;

namespace Application.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseApplication(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExternalContextLanguageMiddleware>();
        return app;
    }
}