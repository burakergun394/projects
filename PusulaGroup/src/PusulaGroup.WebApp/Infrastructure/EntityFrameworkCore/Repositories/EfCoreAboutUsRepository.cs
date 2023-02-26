using PusulaGroup.WebApp.Application.Interfaces.Repositories;
using PusulaGroup.WebApp.Domain.Entities;
using PusulaGroup.WebApp.Infrastructure.EntityFrameworkCore.Contexts;

namespace PusulaGroup.WebApp.Infrastructure.EntityFrameworkCore.Repositories
{
    public class EfCoreAboutUsRepository : EfCoreBaseRepository<AboutUs, ApplicationDbContext>, IAboutUsRepository
    {
        public EfCoreAboutUsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
