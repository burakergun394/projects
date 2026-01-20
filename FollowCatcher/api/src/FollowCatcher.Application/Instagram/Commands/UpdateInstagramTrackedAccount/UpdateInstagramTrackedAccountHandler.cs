using FollowCatcher.Domain.Data;
using FollowCatcher.Domain.Instagram;
using MediatR;

namespace FollowCatcher.Application.Instagram.Commands.UpdateInstagramTrackedAccount;

public class UpdateInstagramTrackedAccountHandler(
    IInstagramTrackedAccountRepository repository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateInstagramTrackedAccountCommand>
{
    public async Task Handle(UpdateInstagramTrackedAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Instagram tracked account with ID {request.Id} not found.");

        account.UpdateUsername(request.Username);

        repository.Update(account);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
