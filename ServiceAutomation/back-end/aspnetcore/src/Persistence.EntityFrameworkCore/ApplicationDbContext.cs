using Domain.Contexts;
using Domain.Products;
using Domain.Shared;
using Domain.Shared.Entities;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityFrameworkCore;

internal class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly IContextService _contextService;

    public ApplicationDbContext(DbContextOptions options, IContextService contextService) : base(options)
    {
        _contextService = contextService;
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(EntityFrameworkCoreConstants.DefaultSchemaName);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.Entity<Product>().HasQueryFilter(x => x.TenantCode == GetTenantCode());
        modelBuilder.Entity<User>().HasQueryFilter(x => x.TenantCode == GetTenantCode());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries();

        foreach (var entry in entries)
        {
            if (entry.Entity is TenantEntity<Guid> tenantGuidEntity)
                tenantGuidEntity.TenantCode = GetTenantCode();

            if (entry.Entity is TenantEntity<int> tenantIdEntity)
                tenantIdEntity.TenantCode = GetTenantCode();

            if (entry.Entity is IAuditableEntity auditableEntity)
            {
                if (entry.State is EntityState.Added)
                {
                    auditableEntity.CreatedBy = GetUserCode();
                    auditableEntity.CreatedAt = DateTime.UtcNow;
                    auditableEntity.LastUpdatedBy = "";
                    auditableEntity.LastUpdatedAt = default;
                }

                if (entry.State is EntityState.Modified)
                {
                    auditableEntity.LastUpdatedBy = GetUserCode();
                    auditableEntity.LastUpdatedAt = DateTime.UtcNow;
                }
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    private string GetTenantCode()
    {
        var context = _contextService.GetContext();
        string tenantCode = "DEFAULT";
        if (context is not null && !string.IsNullOrWhiteSpace(context.TenantCode))
            tenantCode = context.TenantCode;
        return tenantCode;
    }

    private string GetUserCode()
    {
        var context = _contextService.GetContext();
        string userCode = "DEFAULT";
        if (context is not null && !string.IsNullOrWhiteSpace(context.UserCode))
            userCode = context.UserCode;
        return userCode;
    }
}