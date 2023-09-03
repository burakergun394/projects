using Ardalis.GuardClauses;
using Domain.Claims;
using Domain.Responses;
using FluentValidation;
using MediatR;

namespace Application.Roles.Commands.Create;

public record ClaimCreateCommand(string Name) : IRequest<Response<Claim>>;

public class ClaimCreateCommandHandler : IRequestHandler<ClaimCreateCommand, Response<Claim>>
{
    private readonly IClaimRepository _claimRepository;

    public ClaimCreateCommandHandler(IClaimRepository claimRepository)
    {
        _claimRepository = claimRepository;
    }

    public async Task<Response<Claim>> Handle(ClaimCreateCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request);

        var isExist = await _claimRepository.IsClaimExistByName(request.Name);
        if (isExist)
            Response<Claim>.Failure($"Cannot add a duplicate claim name({request.Name})");

        var adding = Claim.Create(request.Name);
        var added = await _claimRepository.CreateAsync(adding, cancellationToken);
        await _claimRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Response<Claim>.Success(added);
    }
}

public class ClaimCreateCommandValidator : AbstractValidator<ClaimCreateCommand>
{
    public ClaimCreateCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Claim name is required");
    }
}