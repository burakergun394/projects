namespace PersonnelTransport.Persistence;

using Microsoft.EntityFrameworkCore;
using PersonnelTransport.Domain.Personnel;
using PersonnelTransport.Domain.Routes;
using PersonnelTransport.Domain.Vehicles;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Personnel> Personnel => Set<Personnel>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<Route> Routes => Set<Route>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
