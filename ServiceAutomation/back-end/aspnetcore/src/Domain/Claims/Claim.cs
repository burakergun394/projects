using Domain.Shared.Entities;
using Domain.Shared.Enums;

namespace Domain.Claims;

public class Claim : AuditableEntity<Guid>
{
    public string Name { get; private set; }

    public string NormalizedName { get; private set; }

    public Claim()
    {
    }

    public Claim(Guid id, Status status, string name) : base(id, status)
    {
        Name = name;
        NormalizedName = Name.ToUpperInvariant();
    }

    public static Claim Create(string name)
    {
        var claim = new Claim(Guid.NewGuid(), Status.Active, name);
        return claim;
    }
}