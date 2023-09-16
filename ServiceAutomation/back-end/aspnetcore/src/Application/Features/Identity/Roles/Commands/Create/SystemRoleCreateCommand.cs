using Application.Abstractions.Localizations;
using Application.Behaviors.Authorization;
using Application.Features.Identity.Users.Commands.Create;
using Ardalis.GuardClauses;
using Domain.Identity.Roles;
using Domain.Shared.Enums;
using Domain.Shared.Responses;
using FluentValidation;
using MediatR;

namespace Application.Features.Identity.Roles.Commands.Create;

public record SystemRoleCreateCommand(string TenantCode, string Name) : IRequest<Response<Role>>, IAuthorizationClaimRequest
{
    public string Claim { get; } = "system.role.create";
}

public class SystemRoleCreateCommandHandler : IRequestHandler<SystemRoleCreateCommand, Response<Role>>
{
    private readonly IRoleRepository _roleRepository;
    private readonly ILocalizationService _localizationService;

    public SystemRoleCreateCommandHandler(IRoleRepository roleRepository, ILocalizationService localizationService)
    {
        _roleRepository = roleRepository;
        _localizationService = localizationService;
    }

    public async Task<Response<Role>> Handle(SystemRoleCreateCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request);

        var isExist = await _roleRepository.IsRoleExist(request.Name, request.TenantCode);
        if (isExist)
            return Response<Role>.Failure(_localizationService["RoleAlreadyExist:{0}", request.Name]);

        var adding = new Role(request.TenantCode, Guid.NewGuid(), Status.Active, request.Name);
        var added = await _roleRepository.CreateAsync(adding, cancellationToken);
        await _roleRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Response<Role>.Success(added, _localizationService["RoleCreated:{0}", request.Name]);
    }
}

public class SystemRoleCreateCommandValidator : AbstractValidator<SystemRoleCreateCommand>
{
    public SystemRoleCreateCommandValidator(ILocalizationService localizationService)
    {
        RuleFor(x => x.TenantCode)
            .NotEmpty()
            .NotNull()
            .WithMessage(localizationService["Required:{0}", nameof(UserCreateCommand.TenantCode)]);

        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage(localizationService["Required:{0}", nameof(UserCreateCommand.Name)]);
    }
}