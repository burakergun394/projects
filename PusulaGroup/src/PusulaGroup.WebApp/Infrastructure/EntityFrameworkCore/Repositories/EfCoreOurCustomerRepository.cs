using PusulaGroup.WebApp.Application.Interfaces.Repositories;
using PusulaGroup.WebApp.Domain.Entities;
using PusulaGroup.WebApp.Infrastructure.EntityFrameworkCore.Contexts;

namespace PusulaGroup.WebApp.Infrastructure.EntityFrameworkCore.Repositories
{
    public class EfCoreOurCustomerRepository : EfCoreBaseRepository<OurCustomer, ApplicationDbContext>, IOurCustomerRepository
    {
        public EfCoreOurCustomerRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
