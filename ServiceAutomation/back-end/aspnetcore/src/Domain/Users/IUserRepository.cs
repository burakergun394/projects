using Domain.Shared;

namespace Domain.Users;

public interface IUserRepository : IRepository<User, Guid>
{
    Task<User> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
}