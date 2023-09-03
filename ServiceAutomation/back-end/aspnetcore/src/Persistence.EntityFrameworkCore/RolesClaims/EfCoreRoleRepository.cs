using Domain.RolesClaims;
using Persistence.EntityFrameworkCore.Shared;

namespace Persistence.EntityFrameworkCore.RolesClaims;

internal class EfCoreRoleClaimRepository : EfCoreRepository<RoleClaim, ApplicationDbContext>, IRoleClaimRepository
{
    public EfCoreRoleClaimRepository(ApplicationDbContext context) : base(context)
    {
         
    }

    public async Task<RoleClaim> GetByRoleIdAsync(Guid roleId, CancellationToken cancellationToken = default) => 
        await GetByPredicateAsync(x => x.RoleId == roleId, cancellationToken);
}