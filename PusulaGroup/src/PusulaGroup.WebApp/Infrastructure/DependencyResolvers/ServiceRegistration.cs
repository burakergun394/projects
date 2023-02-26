using Microsoft.EntityFrameworkCore;
using PusulaGroup.WebApp.Application.Interfaces.Repositories;
using PusulaGroup.WebApp.Application.Interfaces.UnitOfWork;
using PusulaGroup.WebApp.Infrastructure.EntityFrameworkCore.Contexts;
using PusulaGroup.WebApp.Infrastructure.EntityFrameworkCore.Repositories;
using PusulaGroup.WebApp.Infrastructure.EntityFrameworkCore.UnitOfWork;

namespace PusulaGroup.WebApp.Infrastructure.DependencyResolvers
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
            services.AddTransient<IAboutUsRepository, EfCoreAboutUsRepository>();
            services.AddTransient<IGeneralInfoRepository, EfCoreGeneralInfoRepository>();
            services.AddTransient<IOurCustomerRepository, EfCoreOurCustomerRepository>();
            services.AddTransient<IUnitOfWork, EfCoreUnitOfWork>();

            return services;
        }
    }
}
