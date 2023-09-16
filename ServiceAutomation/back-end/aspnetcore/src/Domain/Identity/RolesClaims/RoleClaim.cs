using Domain.Shared.Entities;
using Domain.Shared.Enums;

namespace Domain.Identity.RolesClaims;

public class RoleClaim : AuditableEntity
{

    public Guid RoleId { get; private set; }
    public Guid ClaimId { get; private set; }

    public RoleClaim()
    {
    }

    public RoleClaim(Status status, Guid roleId, Guid claimId) : base(status)
    {
        RoleId = roleId;
        ClaimId = claimId;
    }

    public static RoleClaim Create(Guid roleId, Guid claimId)
    {
        var rolesClaims = new RoleClaim(Status.Active, roleId, claimId);
        return rolesClaims;
    }
}
