using Ardalis.GuardClauses;
using Domain.Responses;
using Domain.RolesClaims;
using FluentValidation;
using MediatR;

namespace Application.RolesClaims.Commands.Create;

public record RoleClaimCreateCommand(Guid RoleId, Guid ClaimId) : IRequest<Response<RoleClaim>>;

public class RoleClaimCreateCommandHandler : IRequestHandler<RoleClaimCreateCommand, Response<RoleClaim>>
{
    private readonly IRoleClaimRepository _roleClaimRepository;

    public RoleClaimCreateCommandHandler(IRoleClaimRepository roleClaimRepository)
    {
        _roleClaimRepository = roleClaimRepository;
    }

    public async Task<Response<RoleClaim>> Handle(RoleClaimCreateCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request);

        var adding = RoleClaim.Create(request.RoleId, request.ClaimId);
        var added = await _roleClaimRepository.CreateAsync(adding, cancellationToken);
        await _roleClaimRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Response<RoleClaim>.Success(added);
    }
}

public class RoleClaimCreateCommandValidator : AbstractValidator<RoleClaimCreateCommand>
{
    public RoleClaimCreateCommandValidator()
    {
        RuleFor(x => x.RoleId)
            .NotEmpty()
            .WithMessage("Role id is required");

        RuleFor(x => x.ClaimId)
          .NotEmpty()
          .WithMessage("Claim id is required");
    }
}