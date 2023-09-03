using Domain.Shared.Enums;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityFrameworkCore.Shared;

namespace Persistence.EntityFrameworkCore.Users;

internal class EfCoreUserRepository : EfCoreRepository<User, Guid, ApplicationDbContext>, IUserRepository
{

    public EfCoreUserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<User> GetByUsernameAsync(string username, CancellationToken cancellationToken = default) => await GetByPredicateAsync(x => x.Username == username, cancellationToken);

    public async Task<GetPassword> GetPasswordByUsernameAsync(string username, CancellationToken cancellationToken = default)
        => await GetOfSelectedColumnsByPredicateAsync(x => x.NormalizedUsername == username.ToUpperInvariant(), x => new GetPassword
        {
            PasswordHash = x.PasswordHash,
            PasswordSalt = x.PasswordSalt
        }, cancellationToken);

    public Task<List<string>> GetClaimsByUsername(string username, CancellationToken cancellationToken = default)
    {
        var tenantCode = Context.GetTenantCode();

        var query = from user in Context.Users.IgnoreQueryFilters()
                    join role in Context.Roles.IgnoreQueryFilters()
                        on user.RoleId equals role.Id
                    join roleClaim in Context.RolesClaims.IgnoreQueryFilters()
                        on role.Id equals roleClaim.RoleId
                    join claim in Context.Claims.IgnoreQueryFilters()
                        on roleClaim.ClaimId equals claim.Id
                    where
                        user.TenantCode == tenantCode
                        && role.TenantCode == tenantCode
                        && user.Username == username
                        && user.Status == Status.Active
                        && role.Status == Status.Active
                        && roleClaim.Status == Status.Active
                        && claim.Status == Status.Active
                    select claim.NormalizedName;

        return query.ToListAsync();
    }

    public Task<bool> IsUserExistByUsername(string username, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
