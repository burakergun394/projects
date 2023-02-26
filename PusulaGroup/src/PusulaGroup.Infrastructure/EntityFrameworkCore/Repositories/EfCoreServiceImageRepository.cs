using PusulaGroup.Application.Interfaces.Repositories;
using PusulaGroup.Domain.Entities;
using PusulaGroup.Infrastructure.EntityFrameworkCore.Contexts;

namespace PusulaGroup.Infrastructure.EntityFrameworkCore.Repositories
{
    public class EfCoreServiceImageRepository : EfCoreBaseRepository<ServiceImage, ApplicationDbContext>, IServiceImageRepository
    {
        public EfCoreServiceImageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
