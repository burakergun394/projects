using Application.Abstractions.Localizations;
using Application.Behaviors.Authorization;
using Domain.Identity.Roles;
using Domain.Shared.Responses;
using FluentValidation;
using MediatR;

namespace Application.Features.Identity.Roles.Queries.GetById;

public record RoleGetByIdQuery(Guid Id) : IRequest<Response<Role>>, IAuthorizationClaimRequest
{
    public string Claim { get; } = "role.get";
}

public class RoleGetByIdQueryHandler : IRequestHandler<RoleGetByIdQuery, Response<Role>>
{
    private readonly IRoleRepository _roleRepository;
    private readonly ILocalizationService _localizationService;

    public RoleGetByIdQueryHandler(IRoleRepository roleRepository, ILocalizationService localizationService)
    {
        _roleRepository = roleRepository;
        _localizationService = localizationService;
    }

    public async Task<Response<Role>> Handle(RoleGetByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(request.Id, cancellationToken);
        return role is null 
            ? Response<Role>.Failure(_localizationService["RoleNotFound"]) 
            : Response<Role>.Success(role);
    }
}

public class RoleGetByIdQueryValidator : AbstractValidator<RoleGetByIdQuery>
{
    public RoleGetByIdQueryValidator(ILocalizationService localizationService)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .WithMessage(localizationService["Required:{0}", nameof(RoleGetByIdQuery.Id)]);
    }
}