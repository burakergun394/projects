using Microsoft.AspNetCore.Builder;

namespace Infrastructure.Auth.Microsoft.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseAuthMicrosoft(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }
}