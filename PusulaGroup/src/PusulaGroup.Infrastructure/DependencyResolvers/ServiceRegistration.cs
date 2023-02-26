using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PusulaGroup.Application.Interfaces.Repositories;
using PusulaGroup.Application.Interfaces.UnitOfWork;
using PusulaGroup.Infrastructure.EntityFrameworkCore.Contexts;
using PusulaGroup.Infrastructure.EntityFrameworkCore.Repositories;
using PusulaGroup.Infrastructure.EntityFrameworkCore.UnitOfWork;

namespace PusulaGroup.Infrastructure.DependencyResolvers
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureLayerServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddTransient<ITourRepository, EfCoreTourRepository>();
            services.AddTransient<ITourImageRepository, EfCoreTourImageRepository>();
            services.AddTransient<IServiceRepository, EfCoreServiceRepository>();
            services.AddTransient<IServiceImageRepository, EfCoreServiceImageRepository>();
            services.AddTransient<IUnitOfWork, EfCoreUnitOfWork>();

            return services;
        }
    }
}
