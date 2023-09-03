using Domain.Users;
using Persistence.EntityFrameworkCore.Shared;

namespace Persistence.EntityFrameworkCore.Users;

internal class EfCoreUserRepository : EfCoreRepository<User, Guid, ApplicationDbContext>, IUserRepository
{
    public EfCoreUserRepository(ApplicationDbContext context) : base(context)
    {
         
    }

    public async Task<User> GetByCodeAsync(string username, CancellationToken cancellationToken = default) => await GetByPredicateAsync(x => x.Username == username, cancellationToken);
}
