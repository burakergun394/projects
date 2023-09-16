using Domain.Identity.Tenants;
using Persistence.EntityFrameworkCore.Shared;

namespace Persistence.EntityFrameworkCore.Identity.Tenants;

internal class EfCoreTenantRepository : EfCoreRepository<Tenant, ApplicationDbContext>, ITenantRepository
{
    public EfCoreTenantRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<string>> GetActiveListAsync(CancellationToken cancellationToken = default)
        => await GetListOfSelectedColumnsAsync(x => x.TenantCode, cancellationToken);
}