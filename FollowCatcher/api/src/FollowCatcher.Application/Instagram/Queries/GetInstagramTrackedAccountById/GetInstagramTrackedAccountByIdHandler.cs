using FollowCatcher.Application.Instagram.Dtos;
using FollowCatcher.Domain.Instagram;
using MediatR;

namespace FollowCatcher.Application.Instagram.Queries.GetInstagramTrackedAccountById;

public class GetInstagramTrackedAccountByIdHandler(
    IInstagramTrackedAccountRepository repository) : IRequestHandler<GetInstagramTrackedAccountByIdQuery, InstagramTrackedAccountDto?>
{
    public async Task<InstagramTrackedAccountDto?> Handle(GetInstagramTrackedAccountByIdQuery request, CancellationToken cancellationToken)
    {
        var account = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (account is null)
            return null;

        return new InstagramTrackedAccountDto(
            account.Id,
            account.Username,
            account.GetFollowingCount(),
            account.LastChecked,
            account.CreatedAt,
            account.UpdatedAt);
    }
}
