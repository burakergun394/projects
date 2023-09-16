using Application.Abstractions.Localizations;
using Application.Behaviors.Authorization;
using Domain.Identity.Claims;
using Domain.Shared.Responses;
using FluentValidation;
using MediatR;

namespace Application.Features.Identity.Claims.Queries.GetById;

public record ClaimGetByIdQuery(Guid Id) : IRequest<Response<Claim>>, IAuthorizationClaimRequest
{
    public string Claim { get; } = "claim.get";
}

public class ClaimGetByIdQueryHandler : IRequestHandler<ClaimGetByIdQuery, Response<Claim>>
{
    private readonly IClaimRepository _claimRepository;
    private readonly ILocalizationService _localizationService;

    public ClaimGetByIdQueryHandler(IClaimRepository claimRepository, ILocalizationService localizationService)
    {
        _claimRepository = claimRepository;
        _localizationService = localizationService;
    }

    public async Task<Response<Claim>> Handle(ClaimGetByIdQuery request, CancellationToken cancellationToken)
    {
        var claim = await _claimRepository.GetByIdAsync(request.Id, cancellationToken);
        return claim is null 
            ? Response<Claim>.Failure(_localizationService["ClaimNotFound"]) 
            : Response<Claim>.Success(claim);
    }
}

public class ClaimGetByIdQueryValidator : AbstractValidator<ClaimGetByIdQuery>
{
    public ClaimGetByIdQueryValidator(ILocalizationService localizationService)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .WithMessage(localizationService["Required:{0}", nameof(ClaimGetByIdQuery.Id)]);
    }
}