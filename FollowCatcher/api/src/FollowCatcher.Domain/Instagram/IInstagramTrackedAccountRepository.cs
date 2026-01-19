using FollowCatcher.Domain.Common;

namespace FollowCatcher.Domain.Instagram;

public interface IInstagramTrackedAccountRepository : IRepository<InstagramTrackedAccount>
{
    // specific methods if needed, but IRepository<T> covers GetAllAsync
}
