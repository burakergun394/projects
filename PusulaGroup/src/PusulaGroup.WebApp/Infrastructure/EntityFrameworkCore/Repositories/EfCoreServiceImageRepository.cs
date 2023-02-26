using PusulaGroup.WebApp.Application.Interfaces.Repositories;
using PusulaGroup.WebApp.Domain.Entities;
using PusulaGroup.WebApp.Infrastructure.EntityFrameworkCore.Contexts;

namespace PusulaGroup.WebApp.Infrastructure.EntityFrameworkCore.Repositories
{
    public class EfCoreServiceImageRepository : EfCoreBaseRepository<ServiceImage, ApplicationDbContext>, IServiceImageRepository
    {
        public EfCoreServiceImageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
