using PusulaGroup.WebApp.Application.Interfaces.Repositories;
using PusulaGroup.WebApp.Domain.Entities;
using PusulaGroup.WebApp.Infrastructure.EntityFrameworkCore.Contexts;

namespace PusulaGroup.WebApp.Infrastructure.EntityFrameworkCore.Repositories
{
    public class EfCoreServiceRepository : EfCoreBaseRepository<Service, ApplicationDbContext>, IServiceRepository
    {
        public EfCoreServiceRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
