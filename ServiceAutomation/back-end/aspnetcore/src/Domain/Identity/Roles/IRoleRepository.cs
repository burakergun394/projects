using Domain.Shared;

namespace Domain.Identity.Roles;

public interface IRoleRepository : IRepository<Role, Guid>
{
    Task<bool> IsRoleExist(string name, CancellationToken cancellationToken = default);

    Task<bool> IsRoleExist(string name, string tenantCode, CancellationToken cancellationToken = default);
}