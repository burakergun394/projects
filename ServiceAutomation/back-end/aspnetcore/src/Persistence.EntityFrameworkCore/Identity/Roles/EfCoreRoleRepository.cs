using Domain.Identity.Roles;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityFrameworkCore.Shared;

namespace Persistence.EntityFrameworkCore.Identity.Roles;

internal class EfCoreRoleRepository : EfCoreRepository<Role, Guid, ApplicationDbContext>, IRoleRepository
{
    public EfCoreRoleRepository(ApplicationDbContext context) : base(context)
    {

    }

    public async Task<bool> IsRoleExist(string name, CancellationToken cancellationToken = default) 
        => await AnyByPredicateAsync(x => x.NormalizedName == name.ToUpperInvariant(), cancellationToken);

    public async Task<bool> IsRoleExist(string name, string tenantCode, CancellationToken cancellationToken = default) 
        => await AnyByPredicateAsync(x => x.NormalizedName == name.ToUpperInvariant() && x.TenantCode == tenantCode.ToUpperInvariant(), cancellationToken, true);

}