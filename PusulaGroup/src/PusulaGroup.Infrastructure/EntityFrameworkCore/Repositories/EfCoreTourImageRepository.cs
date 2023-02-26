using PusulaGroup.Application.Interfaces.Repositories;
using PusulaGroup.Domain.Entities;
using PusulaGroup.Infrastructure.EntityFrameworkCore.Contexts;

namespace PusulaGroup.Infrastructure.EntityFrameworkCore.Repositories
{
    public class EfCoreTourImageRepository : EfCoreBaseRepository<TourImage, ApplicationDbContext>, ITourImageRepository
    {
        public EfCoreTourImageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
