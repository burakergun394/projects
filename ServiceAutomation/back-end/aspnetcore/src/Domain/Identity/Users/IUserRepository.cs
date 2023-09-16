using Domain.Shared;

namespace Domain.Identity.Users;

public interface IUserRepository : IRepository<User, Guid>
{
    Task<List<string>> GetClaimsAsync(string tenantCode, string username, CancellationToken cancellationToken = default);

    Task<bool> IsUserExistAsync(string tenantCode, string username, CancellationToken cancellationToken = default);

    Task<GetPassword> GetPasswordAsync(string tenantCode, string username, CancellationToken cancellationToken = default);
}