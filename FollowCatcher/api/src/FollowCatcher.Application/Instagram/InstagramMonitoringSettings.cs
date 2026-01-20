namespace FollowCatcher.Application.Instagram;

public class InstagramMonitoringSettings
{
    public const string SectionName = "Instagram:Monitoring";

    public TimeSpan CheckInterval { get; set; } = TimeSpan.FromMinutes(5);
}
