using Domain.Identity.RolesClaims;
using Persistence.EntityFrameworkCore.Shared;

namespace Persistence.EntityFrameworkCore.Identity.RolesClaims;

internal class EfCoreRoleClaimRepository : EfCoreRepository<RoleClaim, ApplicationDbContext>, IRoleClaimRepository
{
    public EfCoreRoleClaimRepository(ApplicationDbContext context) : base(context)
    {

    }

    public async Task<RoleClaim> GetByRoleIdAsync(Guid roleId, CancellationToken cancellationToken = default) =>
        await GetByPredicateAsync(x => x.RoleId == roleId, cancellationToken);

    public async Task<bool> IsRoleClaimExist(Guid roleId, Guid claimId) =>
        await AnyByPredicateAsync(x => x.ClaimId == claimId && x.RoleId == roleId);
}