using Domain.Shared;

namespace Domain.Users;

public interface IUserRepository : IRepository<User, Guid>
{
    Task<User> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);

    Task<List<string>> GetClaimsByUsername(string username, CancellationToken cancellationToken = default);

    Task<bool> IsUserExistByUsername(string username, CancellationToken cancellationToken = default);

    Task<GetPassword> GetPasswordByUsernameAsync(string username, CancellationToken cancellationToken = default);
}