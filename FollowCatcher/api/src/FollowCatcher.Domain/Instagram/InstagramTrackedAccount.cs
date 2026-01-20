using FollowCatcher.Domain.Common;
using System.Text.Json;

namespace FollowCatcher.Domain.Instagram;

public class InstagramTrackedAccount(string username) : Entity
{
    public string Username { get; private set; } = username;
    public string FollowingIdsJson { get; private set; } = "[]"; // Serialized List<string>
    public DateTime LastChecked { get; private set; } = DateTime.UtcNow;

    public void UpdateUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username cannot be empty.", nameof(username));

        Username = username;
        MarkAsUpdated();
    }

    public void UpdateFollowingAndDetectChanges(List<string> newFollowing, byte[] monitoredProfileCardInfo)
    {
        var previousFollowing = GetCurrentFollowing();
        var newFollowers = newFollowing.Except(previousFollowing).ToList();
        if (previousFollowing.Count != 0)
        {
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
        }

        FollowingIdsJson = JsonSerializer.Serialize(newFollowing);
        LastChecked = DateTime.UtcNow;
        MarkAsUpdated();
    }

    public int GetFollowingCount()
    {
        return GetCurrentFollowing().Count;
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
