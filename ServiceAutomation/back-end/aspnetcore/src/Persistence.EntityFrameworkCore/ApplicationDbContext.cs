using Domain.Identity.Claims;
using Domain.Identity.Roles;
using Domain.Identity.RolesClaims;
using Domain.Identity.Users;
using Domain.Products;
using Domain.Shared;
using Domain.Shared.Contexts;
using Domain.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityFrameworkCore;

internal class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly IExternalContextService _contextService;

    public ApplicationDbContext(DbContextOptions options, IExternalContextService contextService) : base(options)
    {
        _contextService = contextService;
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Claim> Claims { get; set; }
    public DbSet<RoleClaim> RolesClaims { get; set; }

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
            {
                if (entry.State is EntityState.Added && string.IsNullOrWhiteSpace(tenantGuidEntity.TenantCode))
                {
                    tenantGuidEntity.TenantCode = GetTenantCode();
                }

                if (entry.State is EntityState.Modified)
                {
                    tenantGuidEntity.TenantCode = GetTenantCode();
                }
            }


            if (entry.Entity is TenantEntity<int> tenantIdEntity)
            {
                if (entry.State is EntityState.Added && string.IsNullOrWhiteSpace(tenantIdEntity.TenantCode))
                {
                    tenantIdEntity.TenantCode = GetTenantCode();
                }

                if (entry.State is EntityState.Modified)
                {
                    tenantIdEntity.TenantCode = GetTenantCode();
                }
            }

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

    internal string GetTenantCode()
    {
        var context = _contextService.GetContext();
        string tenantCode = "SYSTEM";
        if (context is not null && !string.IsNullOrWhiteSpace(context.TenantCode))
            tenantCode = context.TenantCode;
        return tenantCode;
    }

    internal string GetUserCode()
    {
        var context = _contextService.GetContext();
        string userCode = "SYSTEM";
        if (context is not null && !string.IsNullOrWhiteSpace(context.Username))
            userCode = context.Username;
        return userCode;
    }
}