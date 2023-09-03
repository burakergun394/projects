using Domain.Claims;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityFrameworkCore.Shared;

namespace Persistence.EntityFrameworkCore.Claims;

internal class EfCoreClaimRepository : EfCoreRepository<Claim, Guid, ApplicationDbContext>, IClaimRepository
{
    public EfCoreClaimRepository(ApplicationDbContext context) : base(context)
    {

    }

    public async Task<bool> IsClaimExistByName(string name) => await DbSet.AnyAsync(x => x.NormalizedName == name.ToUpperInvariant());
}
