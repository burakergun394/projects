using Domain.Shared;

namespace Domain.Identity.RolesClaims;

public interface IRoleClaimRepository : IRepository<RoleClaim>
{
    Task<RoleClaim> GetByRoleIdAsync(Guid roleId, CancellationToken cancellationToken = default);

    Task<bool> IsRoleClaimExist(Guid roleId, Guid claimId);
}