using FollowCatcher.Application.Instagram.Queries.GetInstagramProfile;
using FollowCatcher.Domain.Data;
using FollowCatcher.Domain.Instagram;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Space.Abstraction;

namespace FollowCatcher.Application.Instagram.BackgroundServices;

public class InstagramTrackedAccountWorker(
    IServiceProvider serviceProvider,
    ILogger<InstagramTrackedAccountWorker> logger) : BackgroundService
{
    private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(5);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Instagram Monitoring Worker started");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await MonitorAccountsAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while monitoring Instagram accounts");
            }

            await Task.Delay(_checkInterval, stoppingToken);
        }

        logger.LogInformation("Instagram Monitoring Worker stopped");
    }

    private const int MaxConcurrentTasks = 10;

    private async Task MonitorAccountsAsync(CancellationToken cancellationToken)
    {
        IEnumerable<InstagramTrackedAccount> monitoredAccounts;

        using (var scope = serviceProvider.CreateScope())
        {
            var repository = scope.ServiceProvider.GetRequiredService<IInstagramTrackedAccountRepository>();
            monitoredAccounts = await repository.GetAllAsync(cancellationToken);
        }

        var accountList = monitoredAccounts.ToList();
        logger.LogInformation("Monitoring {Count} Instagram accounts with {MaxTasks} concurrent tasks",
            accountList.Count, MaxConcurrentTasks);

        var options = new ParallelOptions
        {
            MaxDegreeOfParallelism = MaxConcurrentTasks,
            CancellationToken = cancellationToken
        };

        await Parallel.ForEachAsync(accountList, options, async (account, ct) =>
        {
            try
            {
                await MonitorSingleAccountWithScopeAsync(account, ct);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error monitoring account {Username}", account.Username);
            }
        });
    }

    private async Task MonitorSingleAccountWithScopeAsync(
        InstagramTrackedAccount account,
        CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();

        var instagramService = scope.ServiceProvider.GetRequiredService<IInstagramService>();
        var cardGenerator = scope.ServiceProvider.GetRequiredService<IInstagramProfileCardGenerator>();
        var httpClientFactory = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var space = scope.ServiceProvider.GetRequiredService<ISpace>();

        await MonitorSingleAccountAsync(
            account,
            instagramService,
            unitOfWork,
            space,
            cancellationToken);
    }

    private async Task MonitorSingleAccountAsync(
        InstagramTrackedAccount account,
        IInstagramService instagramService,
        IUnitOfWork unitOfWork,
        ISpace space,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Checking followers for {Username}", account.Username);

        var currentFollowing = await instagramService.GetUserFollowingAsync(account.Username, cancellationToken);
        if (currentFollowing is null)
        {
            logger.LogWarning("Could not fetch followers for {Username}", account.Username);
            return;
        }

        var profileDto = await space.Send(new GetInstagramProfileQuery(account.Username, true));
        account.UpdateFollowingAndDetectChanges(currentFollowing, profileDto.ProfileCardImage);
        foreach (var domainEvent in account.DomainEvents)
        {
            await space.Publish(domainEvent, cancellationToken);
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);

        account.ClearDomainEvents();

        logger.LogInformation(
            "Updated followers for {Username}. Current count: {Count}",
            account.Username,
            currentFollowing.Count);
    }
}
