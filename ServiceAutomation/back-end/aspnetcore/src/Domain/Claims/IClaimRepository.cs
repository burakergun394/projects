using Domain.Shared;

namespace Domain.Claims;

public interface IClaimRepository : IRepository<Claim, Guid>
{
    Task<bool> IsClaimExistByName(string name);
}