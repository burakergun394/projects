using FollowCatcher.Domain.Common;
using FollowCatcher.Domain.Instagram;

namespace FollowCatcher.Domain.Repositories;

public interface IMonitoredAccountRepository : IRepository<MonitoredAccount>
{
    // specific methods if needed, but IRepository<T> covers GetAllAsync
}
