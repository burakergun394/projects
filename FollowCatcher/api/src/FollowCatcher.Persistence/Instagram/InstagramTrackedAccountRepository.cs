using FollowCatcher.Domain.Instagram;
using FollowCatcher.Persistence.Repositories;

namespace FollowCatcher.Persistence.Instagram;

public class InstagramTrackedAccountRepository(ApplicationDbContext dbContext) : Repository<InstagramTrackedAccount>(dbContext), IInstagramTrackedAccountRepository
{
}
