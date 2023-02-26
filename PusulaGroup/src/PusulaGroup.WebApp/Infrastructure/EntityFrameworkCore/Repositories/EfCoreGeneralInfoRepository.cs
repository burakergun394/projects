using PusulaGroup.WebApp.Application.Interfaces.Repositories;
using PusulaGroup.WebApp.Domain.Entities;
using PusulaGroup.WebApp.Infrastructure.EntityFrameworkCore.Contexts;

namespace PusulaGroup.WebApp.Infrastructure.EntityFrameworkCore.Repositories
{
    public class EfCoreGeneralInfoRepository : EfCoreBaseRepository<GeneralInfo, ApplicationDbContext>, IGeneralInfoRepository
    {
        public EfCoreGeneralInfoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
