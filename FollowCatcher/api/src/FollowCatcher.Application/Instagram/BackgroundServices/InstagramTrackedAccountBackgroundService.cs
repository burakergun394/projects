using FollowCatcher.Domain.Instagram;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Microsoft.Extensions.Options;

namespace FollowCatcher.Application.Instagram.BackgroundServices;

public class InstagramTrackedAccountBackgroundService(
    IServiceScopeFactory serviceScopeFactory,
    IOptions<InstagramMonitoringSettings> options,
    ILogger<InstagramTrackedAccountBackgroundService> logger) : BackgroundService
{
    private readonly TimeSpan _checkInterval = options.Value.CheckInterval;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Instagram Tracked Account Background Service started");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await PublishMonitoringEventsAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while publishing monitoring events");
            }

            await Task.Delay(_checkInterval, stoppingToken);
        }

        logger.LogInformation("Instagram Tracked Account Background Service stopped");
    }

    private const int MaxConcurrentTasks = 10;

    private async Task PublishMonitoringEventsAsync(CancellationToken cancellationToken)
    {
        List<Guid> accountIds;

        using (var scope = serviceScopeFactory.CreateScope())
        {
            var repository = scope.ServiceProvider.GetRequiredService<IInstagramTrackedAccountRepository>();
            var accounts = await repository.GetAllAsync(cancellationToken);
            accountIds = accounts.Select(a => a.Id).ToList();
        }

        logger.LogInformation(
            "Publishing monitoring events for {Count} Instagram accounts with {MaxTasks} concurrent tasks",
            accountIds.Count,
            MaxConcurrentTasks);

        var options = new ParallelOptions
        {
            MaxDegreeOfParallelism = MaxConcurrentTasks,
            CancellationToken = cancellationToken
        };

        await Parallel.ForEachAsync(accountIds, options, async (accountId, ct) =>
        {
            using var scope = serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var monitorEvent = new MonitorInstagramAccountRequestedEvent(accountId);
            await mediator.Publish(monitorEvent, ct);

            logger.LogDebug("Completed monitoring for account {AccountId}", accountId);
        });
    }
}
