using Domain.Shared;
namespace Domain.Identity.Tenants;

public interface ITenantRepository : IRepository<Tenant>
{
    Task<List<string>> GetActiveListAsync(CancellationToken cancellationToken = default);
}