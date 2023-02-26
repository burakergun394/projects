using PusulaGroup.Application.Interfaces.Repositories;
using PusulaGroup.Domain.Entities;
using PusulaGroup.Infrastructure.EntityFrameworkCore.Contexts;

namespace PusulaGroup.Infrastructure.EntityFrameworkCore.Repositories
{
    public class EfCoreTourRepository : EfCoreBaseRepository<Tour, ApplicationDbContext>, ITourRepository
    {
        public EfCoreTourRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
