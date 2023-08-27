using Domain.Contexts;
using Domain.Products;
using Domain.Shared;
using Domain.Shared.Entities;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(EntityFrameworkCoreConstants.DefaultSchemaName);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries();

        foreach (var entry in entries)
        {
            var context = _contextService.GetContext();
            string tenantCode = "DEFAULT";
            string userCode = "DEFAULT";

            if (context is not null)
            {
                if (!string.IsNullOrWhiteSpace(context.TenantCode))
                    tenantCode = context.TenantCode;

                if (!string.IsNullOrWhiteSpace(context.UserCode))
                    userCode = context.UserCode;
            }

            if (entry.Entity is TenantEntity<Guid> tenantGuidEntity)
            {
                tenantGuidEntity.TenantCode = tenantCode;
            }

            if (entry.Entity is TenantEntity<int> tenantIdEntity)
            {
                tenantIdEntity.TenantCode = tenantCode;
            }

            if (entry.Entity is IAuditableEntity auditableEntity)
            {
                if (entry.State is EntityState.Added)
                {
                    auditableEntity.CreatedBy = userCode;
                    auditableEntity.CreatedAt = DateTime.UtcNow;
                    auditableEntity.LastUpdatedBy = "";
                    auditableEntity.LastUpdatedAt = default;
                }

                if (entry.State is EntityState.Modified)
                {
                    auditableEntity.LastUpdatedBy = userCode;
                    auditableEntity.LastUpdatedAt = DateTime.UtcNow;
                }
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}