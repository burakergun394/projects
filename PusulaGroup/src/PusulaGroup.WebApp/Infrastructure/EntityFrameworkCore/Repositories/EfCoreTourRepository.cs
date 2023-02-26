using PusulaGroup.WebApp.Application.Interfaces.Repositories;
using PusulaGroup.WebApp.Domain.Entities;
using PusulaGroup.WebApp.Infrastructure.EntityFrameworkCore.Contexts;

namespace PusulaGroup.WebApp.Infrastructure.EntityFrameworkCore.Repositories
{

    public class EfCoreTourRepository : EfCoreBaseRepository<Tour, ApplicationDbContext>, ITourRepository
    {
        public EfCoreTourRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
