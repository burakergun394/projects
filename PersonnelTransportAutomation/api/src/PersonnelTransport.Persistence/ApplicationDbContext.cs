namespace PersonnelTransport.Persistence;

using Microsoft.EntityFrameworkCore;
using PersonnelTransport.Domain.Employees;
using PersonnelTransport.Domain.Routes;
using PersonnelTransport.Domain.Vehicles;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<Route> Routes => Set<Route>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
