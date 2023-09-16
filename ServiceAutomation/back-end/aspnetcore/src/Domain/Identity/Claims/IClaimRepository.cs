using Domain.Shared;

namespace Domain.Identity.Claims;

public interface IClaimRepository : IRepository<Claim, Guid>
{
    Task<bool> IsClaimExist(string name);
}