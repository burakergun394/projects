using FollowCatcher.Domain.Data;
using FollowCatcher.Domain.Instagram;
using MediatR;

namespace FollowCatcher.Application.Instagram.Commands.CreateInstagramTrackedAccountsBatch;

public class CreateInstagramTrackedAccountsBatchHandler(
    IInstagramTrackedAccountRepository repository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateInstagramTrackedAccountsBatchCommand, List<Guid>>
{
    public async Task<List<Guid>> Handle(CreateInstagramTrackedAccountsBatchCommand request, CancellationToken cancellationToken)
    {
        var ids = new List<Guid>();

        foreach (var username in request.Usernames)
        {
            var account = new InstagramTrackedAccount(username);
            await repository.AddAsync(account, cancellationToken);
            ids.Add(account.Id);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ids;
    }
}
