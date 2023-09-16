using Application.Abstractions.Localizations;
using Application.Behaviors.Authorization;
using Ardalis.GuardClauses;
using Domain.Identity.Claims;
using Domain.Shared.Responses;
using FluentValidation;
using MediatR;

namespace Application.Features.Identity.Claims.Commands.Create;

public record ClaimCreateCommand(string Name) : IRequest<Response<Claim>>, IAuthorizationClaimRequest
{
    public string Claim { get; } = "claim.create";
}

public class ClaimCreateCommandHandler : IRequestHandler<ClaimCreateCommand, Response<Claim>>
{
    private readonly IClaimRepository _claimRepository;
    private readonly ILocalizationService _localizationService;

    public ClaimCreateCommandHandler(IClaimRepository claimRepository, ILocalizationService localizationService)
    {
        _claimRepository = claimRepository;
        _localizationService = localizationService;
    }

    public async Task<Response<Claim>> Handle(ClaimCreateCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request);

        var isExist = await _claimRepository.IsClaimExist(request.Name);
        if (isExist)
            return Response<Claim>.Failure(_localizationService["ClaimAlreadyExist:{0}", request.Name]);

        var adding = Claim.Create(request.Name);
        var added = await _claimRepository.CreateAsync(adding, cancellationToken);
        await _claimRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Response<Claim>.Success(added, _localizationService["ClaimCreated:{0}", request.Name]);
    }
}

public class ClaimCreateCommandValidator : AbstractValidator<ClaimCreateCommand>
{
    public ClaimCreateCommandValidator(ILocalizationService localizationService)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage(localizationService["Required:{0}", nameof(ClaimCreateCommand.Name)]);
    }
}