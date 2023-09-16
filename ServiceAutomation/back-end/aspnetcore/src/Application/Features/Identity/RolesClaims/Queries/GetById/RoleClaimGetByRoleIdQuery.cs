using Application.Abstractions.Localizations;
using Application.Behaviors.Authorization;
using Application.Features.Identity.Claims.Queries.GetById;
using Domain.Identity.RolesClaims;
using Domain.Shared.Responses;
using FluentValidation;
using MediatR;

namespace Application.Features.Identity.RolesClaims.Queries.GetById;

public record RoleClaimGetByRoleIdQuery(Guid RoleId) : IRequest<Response<RoleClaim>>, IAuthorizationClaimRequest
{
    public string Claim { get; } = "roleclaim.get";
}

public class RoleClaimGetByRoleIdQueryHandler : IRequestHandler<RoleClaimGetByRoleIdQuery, Response<RoleClaim>>
{
    private readonly IRoleClaimRepository _roleClaimRepository;
    private readonly ILocalizationService _localizationService;

    public RoleClaimGetByRoleIdQueryHandler(IRoleClaimRepository roleClaimRepository, ILocalizationService localizationService)
    {
        _roleClaimRepository = roleClaimRepository;
        _localizationService = localizationService;
    }

    public async Task<Response<RoleClaim>> Handle(RoleClaimGetByRoleIdQuery request, CancellationToken cancellationToken)
    {
        var roleClaim = await _roleClaimRepository.GetByRoleIdAsync(request.RoleId, cancellationToken);
        return roleClaim is null 
            ? Response<RoleClaim>.Failure(_localizationService["RoleClaimNotFound"]) 
            : Response<RoleClaim>.Success(roleClaim);
    }
}

public class RoleClaimGetByRoleIdQueryValidator : AbstractValidator<RoleClaimGetByRoleIdQuery>
{
    public RoleClaimGetByRoleIdQueryValidator(ILocalizationService localizationService)
    {
        RuleFor(x => x.RoleId)
            .NotEmpty()
            .NotNull()
            .WithMessage(localizationService["Required:{0}", nameof(RoleClaimGetByRoleIdQuery.RoleId)]);
    }
}