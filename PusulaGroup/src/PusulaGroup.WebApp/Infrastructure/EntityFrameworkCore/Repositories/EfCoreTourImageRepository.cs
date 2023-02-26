using PusulaGroup.WebApp.Application.Interfaces.Repositories;
using PusulaGroup.WebApp.Domain.Entities;
using PusulaGroup.WebApp.Infrastructure.EntityFrameworkCore.Contexts;

namespace PusulaGroup.WebApp.Infrastructure.EntityFrameworkCore.Repositories
{
    public class EfCoreTourImageRepository : EfCoreBaseRepository<TourImage, ApplicationDbContext>, ITourImageRepository
    {
        public EfCoreTourImageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
