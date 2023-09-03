using Domain.Shared;

namespace Domain.Users;

public interface IUserRepository : IRepository<User, Guid>
{
    Task<User> GetByCodeAsync(string username, CancellationToken cancellationToken = default);
}