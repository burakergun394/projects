using FollowCatcher.Domain.Instagram;
using FollowCatcher.Domain.Repositories;

namespace FollowCatcher.Persistence.Repositories;

public class MonitoredAccountRepository(ApplicationDbContext dbContext) : Repository<MonitoredAccount>(dbContext), IMonitoredAccountRepository
{
}
