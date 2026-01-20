using FollowCatcher.Application.Instagram.Dtos;
using FollowCatcher.Domain.Instagram;
using MediatR;

namespace FollowCatcher.Application.Instagram.Queries.GetAllInstagramTrackedAccounts;

public class GetAllInstagramTrackedAccountsHandler(
    IInstagramTrackedAccountRepository repository) : IRequestHandler<GetAllInstagramTrackedAccountsQuery, IEnumerable<InstagramTrackedAccountDto>>
{
    public async Task<IEnumerable<InstagramTrackedAccountDto>> Handle(GetAllInstagramTrackedAccountsQuery request, CancellationToken cancellationToken)
    {
        var accounts = await repository.GetAllAsync(cancellationToken);

        return accounts.Select(account => new InstagramTrackedAccountDto(
            account.Id,
            account.Username,
            account.GetFollowingCount(),
            account.LastChecked,
            account.CreatedAt,
            account.UpdatedAt));
    }
}
