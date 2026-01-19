using FollowCatcher.Domain.Common;
using System.Text.Json;

namespace FollowCatcher.Domain.Instagram;

public class InstagramTrackedAccount : Entity
{
    public string Username { get; private set; }
    public string FollowingIdsJson { get; private set; } = "[]"; // Serialized List<string>
    public DateTime LastChecked { get; private set; }

    public InstagramTrackedAccount(string username)
    {
        Username = username;
        LastChecked = DateTime.UtcNow;
    }

    public void UpdateFollowingAndDetectChanges(List<string> newFollowing, byte[] monitoredProfileCardInfo)
    {
        var previousFollowing = GetCurrentFollowing();

        // Detect new followers
        var newFollowers = newFollowing.Except(previousFollowing).ToList();
        foreach (var follower in newFollowers)
        {
            AddDomainEvent(new UserFollowedEvent(Username, follower, monitoredProfileCardInfo));
        }

        // Detect unfollows
        var unfollowed = previousFollowing.Except(newFollowing).ToList();
        foreach (var unfollower in unfollowed)
        {
            AddDomainEvent(new UserUnfollowedEvent(Username, unfollower, monitoredProfileCardInfo));
        }

        FollowingIdsJson = JsonSerializer.Serialize(newFollowing);
        LastChecked = DateTime.UtcNow;
        MarkAsUpdated();
    }

    private List<string> GetCurrentFollowing()
    {
        try
        {
            return JsonSerializer.Deserialize<List<string>>(FollowingIdsJson) ?? [];
        }
        catch
        {
            return [];
        }
    }
}
