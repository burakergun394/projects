using PusulaGroup.Application.Interfaces.Repositories;
using PusulaGroup.Domain.Entities;
using PusulaGroup.Infrastructure.EntityFrameworkCore.Contexts;

namespace PusulaGroup.Infrastructure.EntityFrameworkCore.Repositories
{
    public class EfCoreServiceRepository : EfCoreBaseRepository<Service, ApplicationDbContext>, IServiceRepository
    {
        public EfCoreServiceRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
