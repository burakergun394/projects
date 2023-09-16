using Domain.Identity.Users;
using Domain.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityFrameworkCore.Shared;

namespace Persistence.EntityFrameworkCore.Identity.Users;

internal class EfCoreUserRepository : EfCoreRepository<User, Guid, ApplicationDbContext>, IUserRepository
{

    public EfCoreUserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<GetPassword> GetPasswordAsync(string tenantCode, string username, CancellationToken cancellationToken = default)
        => await GetOfSelectedColumnsByPredicateAsync(x => x.TenantCode == tenantCode && x.NormalizedUsername == username.ToUpperInvariant(), x => new GetPassword
        {
            PasswordHash = x.PasswordHash,
            PasswordSalt = x.PasswordSalt
        }, cancellationToken, true);

    public Task<List<string>> GetClaimsAsync(string tenantCode, string username, CancellationToken cancellationToken = default)
    {
        var query = from user in Context.Users.IgnoreQueryFilters()
                    join role in Context.Roles.IgnoreQueryFilters()
                        on user.RoleId equals role.Id
                    join roleClaim in Context.RolesClaims.IgnoreQueryFilters()
                        on role.Id equals roleClaim.RoleId
                    join claim in Context.Claims.IgnoreQueryFilters()
                        on roleClaim.ClaimId equals claim.Id
                    where
                        user.TenantCode == tenantCode.ToUpperInvariant()
                        && role.TenantCode == tenantCode.ToUpperInvariant()
                        && user.Username == username
                        && user.Status == Status.Active
                        && role.Status == Status.Active
                        && roleClaim.Status == Status.Active
                        && claim.Status == Status.Active
                    select claim.NormalizedName;

        return query.ToListAsync();
    }

    public async Task<bool> IsUserExistAsync(string tenantCode, string username, CancellationToken cancellationToken = default)
        => await AnyByPredicateAsync(x => x.TenantCode == tenantCode.ToUpperInvariant() && x.NormalizedUsername == username.ToUpperInvariant(), cancellationToken, true);
}
