using Domain.Identity.Claims;
using Domain.Identity.Roles;
using Domain.Identity.RolesClaims;
using Domain.Identity.Tenants;
using Domain.Identity.Users;
using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.EntityFrameworkCore.Identity.Claims;
using Persistence.EntityFrameworkCore.Identity.Roles;
using Persistence.EntityFrameworkCore.Identity.RolesClaims;
using Persistence.EntityFrameworkCore.Identity.Tenants;
using Persistence.EntityFrameworkCore.Identity.Users;
using Persistence.EntityFrameworkCore.Products;

namespace Persistence.EntityFrameworkCore.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEntityFrameworkCoreServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IProductRepository, EfCoreProductRepository>();
        services.AddScoped<IUserRepository, EfCoreUserRepository>();
        services.AddScoped<IRoleRepository, EfCoreRoleRepository>();
        services.AddScoped<IClaimRepository, EfCoreClaimRepository>();
        services.AddScoped<IRoleClaimRepository, EfCoreRoleClaimRepository>();
        services.AddScoped<ITenantRepository, EfCoreTenantRepository>();

        return services;
    }
}