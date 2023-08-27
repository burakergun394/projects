using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.EntityFrameworkCore.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseEntityFrameworkCoreMigrate(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();
        return app;
    }
}