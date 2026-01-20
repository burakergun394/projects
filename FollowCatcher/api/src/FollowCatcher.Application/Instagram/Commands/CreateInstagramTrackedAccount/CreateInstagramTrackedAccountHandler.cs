using FollowCatcher.Domain.Data;
using FollowCatcher.Domain.Instagram;
using MediatR;

namespace FollowCatcher.Application.Instagram.Commands.CreateInstagramTrackedAccount;

public class CreateInstagramTrackedAccountHandler(
    IInstagramTrackedAccountRepository repository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateInstagramTrackedAccountCommand, Guid>
{
    public async Task<Guid> Handle(CreateInstagramTrackedAccountCommand request, CancellationToken cancellationToken)
    {
        var account = new InstagramTrackedAccount(request.Username);

        await repository.AddAsync(account, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return account.Id;
    }
}
