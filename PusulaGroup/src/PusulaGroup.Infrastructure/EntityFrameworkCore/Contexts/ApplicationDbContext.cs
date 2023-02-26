using Microsoft.EntityFrameworkCore;
using PusulaGroup.Domain.Entities;
using PusulaGroup.Infrastructure.EntityFrameworkCore.Constants;

namespace PusulaGroup.Infrastructure.EntityFrameworkCore.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourImage> TourImages { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceImage> ServiceImages { get; set; }
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;

            throw new SystemException("DbContext is not configure");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(EntityFrameworkCoreConstants.GetDefaultDbSchema());
            modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
        }
    }
}
