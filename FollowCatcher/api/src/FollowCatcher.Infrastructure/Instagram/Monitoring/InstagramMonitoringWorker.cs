using FollowCatcher.Domain.Data;
using FollowCatcher.Domain.Events;
using FollowCatcher.Domain.Instagram;
using FollowCatcher.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace FollowCatcher.Infrastructure.Instagram.Monitoring;

public class InstagramMonitoringWorker(
    IServiceScopeFactory serviceScopeFactory,
    ILogger<InstagramMonitoringWorker> logger) : BackgroundService
{
    private readonly TimeSpan checkInterval = TimeSpan.FromMinutes(15);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Instagram Monitoring Worker started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await MonitorAccountsAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while monitoring Instagram accounts.");
            }

            await Task.Delay(checkInterval, stoppingToken);
        }

        logger.LogInformation("Instagram Monitoring Worker stopping.");
    }

    private async Task MonitorAccountsAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IMonitoredAccountRepository>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var instagramService = scope.ServiceProvider.GetRequiredService<IInstagramService>();
        var mediator = scope.ServiceProvider.GetRequiredService<IPublisher>();

        var accounts = await repository.GetAllAsync(stoppingToken);

        foreach (var account in accounts)
        {
            try
            {
                logger.LogInformation("Checking following list for {Username}...", account.Username);

                var currentFollowing = await instagramService.GetUserFollowingAsync(account.Username, stoppingToken);
                if (currentFollowing == null)
                {
                    logger.LogWarning("Could not fetch following list for {Username}. Skipping.", account.Username);
                    continue;
                }

                var currentIds = currentFollowing.Select(x => x.Id).ToHashSet();
                var idToUsername = currentFollowing.ToDictionary(x => x.Id, x => x.Username);

                var storedIds = JsonSerializer.Deserialize<List<long>>(account.FollowingIdsJson) ?? [];
                var storedSet = storedIds.ToHashSet();

                var newFollowIds = currentIds.Except(storedSet).ToList();
                var unfollowIds = storedSet.Except(currentIds).ToList();

                foreach (var userId in newFollowIds)
                {
                    var username = idToUsername.GetValueOrDefault(userId, "Unknown");
                    logger.LogInformation("Detected follow: {MonitoredUser} -> {FollowedUser} ({Id})", account.Username, username, userId);
                    await mediator.Publish(new UserFollowedEvent(account.Username, username, userId.ToString()), stoppingToken);
                }

                foreach (var userId in unfollowIds)
                {
                    logger.LogInformation("Detected unfollow: {MonitoredUser} -> {UnfollowedId}", account.Username, userId);
                    await mediator.Publish(new UserUnfollowedEvent(account.Username, "Unknown_Unfollowed", userId.ToString()), stoppingToken);
                }

                if (newFollowIds.Count != 0 || unfollowIds.Count != 0)
                {
                    var newJson = JsonSerializer.Serialize(currentIds.ToList());
                    account.UpdateFollowing(newJson);
                    repository.Update(account);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing account {Username}", account.Username);
            }
        }

        await unitOfWork.SaveChangesAsync(stoppingToken);
    }
}
