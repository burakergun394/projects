using FollowCatcher.Application.Instagram.Queries.GetInstagramProfile;
using FollowCatcher.Domain.Data;
using FollowCatcher.Domain.Instagram;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FollowCatcher.Application.Instagram.Events;

public class InstagramTrackedAccountHandler(
    IInstagramTrackedAccountRepository repository,
    IInstagramService instagramService,
    IUnitOfWork unitOfWork,
    IMediator mediator,
    ILogger<InstagramTrackedAccountHandler> logger) : INotificationHandler<MonitorInstagramAccountRequestedEvent>
{
    public async Task Handle(MonitorInstagramAccountRequestedEvent notification, CancellationToken cancellationToken)
    {
        var account = await repository.GetByIdAsync(notification.AccountId, cancellationToken);
        if (account is null)
        {
            logger.LogWarning("Account {AccountId} not found", notification.AccountId);
            return;
        }

        logger.LogInformation("Checking followers for {Username}", account.Username);

        var currentFollowing = await instagramService.GetUserFollowingAsync(account.Username, cancellationToken);
        if (currentFollowing is null)
        {
            logger.LogWarning("Could not fetch followers for {Username}", account.Username);
            return;
        }

        var profileDto = await mediator.Send(new GetInstagramProfileQuery(account.Username, true), cancellationToken);
        account.UpdateFollowingAndDetectChanges(currentFollowing, profileDto.ProfileCardImage);

        // Collect domain events before clearing
        var domainEvents = account.DomainEvents.ToList();

        logger.LogInformation(
            "Detected {EventCount} changes for {Username}. Current following count: {Count}",
            domainEvents.Count,
            account.Username,
            currentFollowing.Count);

        // Save changes first
        await unitOfWork.SaveChangesAsync(cancellationToken);
        account.ClearDomainEvents();

        foreach (var domainEvent in domainEvents)
        {
            logger.LogInformation(
                "Publishing {EventType} for {Username}",
                domainEvent.GetType().Name,
                account.Username);

            await mediator.Publish(domainEvent, cancellationToken);
        }
    }
}
