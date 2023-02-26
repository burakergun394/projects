using Microsoft.EntityFrameworkCore.Design;

namespace PusulaGroup.WebApp.Infrastructure.EntityFrameworkCore.Contexts
{
    public class DesignTimeApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            return new ApplicationDbContext();
        }
    }
}
