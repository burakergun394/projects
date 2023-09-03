using Domain.Shared;

namespace Domain.RolesClaims;

public interface IRoleClaimRepository : IRepository<RoleClaim>
{
    Task<RoleClaim> GetByRoleIdAsync(Guid roleId, CancellationToken cancellationToken = default);
}