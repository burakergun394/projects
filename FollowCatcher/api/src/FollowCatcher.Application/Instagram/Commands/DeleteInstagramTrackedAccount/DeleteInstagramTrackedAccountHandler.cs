using FollowCatcher.Domain.Data;
using FollowCatcher.Domain.Instagram;
using MediatR;

namespace FollowCatcher.Application.Instagram.Commands.DeleteInstagramTrackedAccount;

public class DeleteInstagramTrackedAccountHandler(
    IInstagramTrackedAccountRepository repository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteInstagramTrackedAccountCommand>
{
    public async Task Handle(DeleteInstagramTrackedAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Instagram tracked account with ID {request.Id} not found.");

        account.MarkAsDeleted();

        repository.Update(account);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
