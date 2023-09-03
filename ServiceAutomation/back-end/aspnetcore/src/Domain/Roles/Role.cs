using Domain.Shared.Entities;
using Domain.Shared.Enums;

namespace Domain.Roles;

public class Role : TenantAuditableEntity<Guid>
{
    public string Name { get; private set; }

    public string NormalizedName { get; private set; }

    public Role()
    {
    }

    public Role(Guid id, Status status, string name) : base(id, status)
    {
        Name = name;
        NormalizedName = Name.ToUpperInvariant();
    }

    public static Role Create(string name)
    {
        var role = new Role(Guid.NewGuid(), Status.Active, name);
        return role;
    }
}