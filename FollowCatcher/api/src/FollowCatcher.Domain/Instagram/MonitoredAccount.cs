using FollowCatcher.Domain.Common;

namespace FollowCatcher.Domain.Instagram;

public class MonitoredAccount : Entity
{
    public string Username { get; private set; }
    public string FollowingIdsJson { get; private set; } = "[]"; // Serialized List<long> or List<string>
    public DateTime LastChecked { get; private set; }

    public MonitoredAccount(string username)
    {
        Username = username;
        LastChecked = DateTime.UtcNow;
    }

    public void UpdateFollowing(string newFollowingJson)
    {
        FollowingIdsJson = newFollowingJson;
        LastChecked = DateTime.UtcNow;
    }
}
