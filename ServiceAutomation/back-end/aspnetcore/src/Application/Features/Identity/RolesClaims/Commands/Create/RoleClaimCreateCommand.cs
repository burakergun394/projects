using Application.Abstractions.Localizations;
using Application.Behaviors.Authorization;
using Ardalis.GuardClauses;
using Domain.Identity.RolesClaims;
using Domain.Shared.Responses;
using FluentValidation;
using MediatR;

namespace Application.Features.Identity.RolesClaims.Commands.Create;

public record RoleClaimCreateCommand(Guid RoleId, Guid ClaimId) : IRequest<Response<RoleClaim>>, IAuthorizationClaimRequest
{
    public string Claim { get; } = "roleclaim.create";
}

public class RoleClaimCreateCommandHandler : IRequestHandler<RoleClaimCreateCommand, Response<RoleClaim>>
{
    private readonly IRoleClaimRepository _roleClaimRepository;
    private readonly ILocalizationService _localizationService;

    public RoleClaimCreateCommandHandler(IRoleClaimRepository roleClaimRepository, ILocalizationService localizationService)
    {
        _roleClaimRepository = roleClaimRepository;
        _localizationService = localizationService;
    }

    public async Task<Response<RoleClaim>> Handle(RoleClaimCreateCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request);

        var isExist = await _roleClaimRepository.IsRoleClaimExist(request.RoleId, request.ClaimId);
        if (isExist)
            return Response<RoleClaim>.Failure(_localizationService["RoleClaimAlreadyExist:{0}", $"{request.RoleId}-{request.ClaimId}"]);

        var adding = RoleClaim.Create(request.RoleId, request.ClaimId);
        var added = await _roleClaimRepository.CreateAsync(adding, cancellationToken);
        await _roleClaimRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Response<RoleClaim>.Success(added, _localizationService["RoleClaimCreated:{0}", $"{request.RoleId}-{request.ClaimId}"]);
    }
}

public class RoleClaimCreateCommandValidator : AbstractValidator<RoleClaimCreateCommand>
{
    public RoleClaimCreateCommandValidator(ILocalizationService localizationService)
    {
        RuleFor(x => x.RoleId)
            .NotEmpty()
            .NotNull()
            .WithMessage(localizationService["Required:{0}", nameof(RoleClaimCreateCommand.RoleId)]);

        RuleFor(x => x.ClaimId)
            .NotEmpty()
            .NotNull()
            .WithMessage(localizationService["Required:{0}", nameof(RoleClaimCreateCommand.ClaimId)]);
    }
}