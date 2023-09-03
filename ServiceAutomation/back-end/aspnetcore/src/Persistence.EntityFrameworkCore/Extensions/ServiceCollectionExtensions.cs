using Domain.Claims;
using Domain.Products;
using Domain.Roles;
using Domain.RolesClaims;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.EntityFrameworkCore.Claims;
using Persistence.EntityFrameworkCore.Products;
using Persistence.EntityFrameworkCore.Roles;
using Persistence.EntityFrameworkCore.RolesClaims;
using Persistence.EntityFrameworkCore.Users;

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

        return services;
    }
}